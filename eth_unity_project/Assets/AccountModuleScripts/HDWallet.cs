﻿using NBitcoin;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.HdWallet;
using UnityEngine;
using System;
using System.IO;
using Nethereum.Signer;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class HDWallet
{
    private static Wallet _wallet;
    private string keystore = Path.Combine(Application.persistentDataPath, "keystore.dat");

    public void createWallet()
    {
        Mnemonic words;
        if (!File.Exists(keystore))
        {
            words = new Mnemonic(Wordlist.English, WordCount.Twelve);
            using (StreamWriter sw = File.CreateText(keystore))
            {

                sw.WriteLine(words.ToString());
            }
        } else
        {
            using (StreamReader sr = File.OpenText(keystore))
            {
                string s;
                s = sr.ReadLine();
                if (s != null)
                {
                    words = new Mnemonic(s);
                } else
                {
                    throw new Exception("Invalid keystore file.");
                }
            }
        }
        Debug.Log("mnemonic: " + words.ToString());
        //string Words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
        string Password1 = "password";
        _wallet = new Wallet(words.ToString(), Password1);
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

    public static string sign(string message)
    {
        var signer = new EthereumMessageSigner();
        var pk = getAccount().PrivateKey;
        var byteArr = StringToBytes(message);
        var sig = signer.Sign(byteArr, new EthECKey(pk));
        Debug.Log("signature: " + sig);
        return sig;
    }

    private static byte[] StringToBytes(string data)
    {
        if (data.StartsWith("0x"))
        {
            data = data.Substring(2);
        }
        SoapHexBinary shb = SoapHexBinary.Parse(data);
        return shb.Value;
    }
}
