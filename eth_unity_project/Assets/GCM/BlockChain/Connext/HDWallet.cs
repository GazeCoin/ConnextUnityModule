using NBitcoin;
using Nethereum.HdWallet;
using UnityEngine;
using System;
using System.IO;
using Nethereum.Signer;
using System.Text;

public class HDWallet
{
    private static Wallet _wallet;
    private readonly string keystore = Path.Combine(Application.persistentDataPath, "keystore.dat");
    private static Mnemonic _mnemonic;

    public void CreateWallet()
    {
        if (!File.Exists(keystore))
        {
            _mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            using (StreamWriter sw = File.CreateText(keystore))
            {
                sw.WriteLine(_mnemonic.ToString());
            }
        }
        else
        {
            using (StreamReader sr = File.OpenText(keystore))
            {
                string s;
                s = sr.ReadLine();
                if (s != null)
                {
                    _mnemonic = new Mnemonic(s);
                }
                else
                {
                    throw new Exception("Invalid keystore file.");
                }
            }
        }
        Debug.Log("mnemonic: " + _mnemonic.ToString());
        //string Words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
        string Password1 = "password";
        _wallet = new Wallet(_mnemonic.ToString(), Password1);
        //for (int i = 0; i < 10; i++)
        //{
        int i = 0;
        var account = _wallet.GetAccount(i);
        Debug.Log("Account index : " + i + " - Address : " + account.Address + " - Private key : " + account.PrivateKey);
        //}

    }

    public static Nethereum.Web3.Accounts.Account getAccount()
    {
        return _wallet.GetAccount(0);
    }

    public static Wallet getInstance()
    {
        return _wallet;
    }

    public static string Sign(string message)
    {
        var byteArr = StringToBytes(message);
        return Sign(byteArr);
    }

    // Sign an array of bytes
    public static string Sign(byte[] bytes)
    {
        var signer = new EthereumMessageSigner();
        var pk = getAccount().PrivateKey;
        var sig = signer.Sign(bytes, new EthECKey(pk));
        Debug.Log("signature: " + sig);
        return sig;
    }

    // Hash and Sign an array of bytes
    public static string HashAndSign(byte[] bytes)
    {
        var signer = new EthereumMessageSigner();
        var pk = getAccount().PrivateKey;
        var sig = signer.HashAndSign(bytes, new EthECKey(pk));
        Debug.Log("signature: " + sig);
        return sig;
    }

    private static byte[] StringToBytes(string data)
    {
        if (data.StartsWith("0x"))
        {
            data = data.Substring(2);
        }
        byte[] bytes = StringToByteArray(data);
        return bytes;
    }

    public static byte[] StringToByteArray(String hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }

    public static string GetXPubKey()
    {
        ExtKey extKey = _mnemonic.DeriveExtKey();
        Debug.Log("exKey to Wif:" + extKey.GetWif(NBitcoin.Network.Main).Neuter().ToString());
        //PubKey xPubKey = extKey.Neuter().PubKey;
        
        String xpub = extKey.GetWif(NBitcoin.Network.Main).Neuter().ToString();
        //string xpub = extKey.Neuter()..PubKey.ToString();
        return xpub;
    }
}
