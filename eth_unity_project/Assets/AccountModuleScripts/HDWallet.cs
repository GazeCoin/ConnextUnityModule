using NBitcoin;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.HdWallet;
using UnityEngine;
using System;

public class HDWallet
{
    private Wallet _wallet;

    public void createWallet()
    {
        string Words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
        string Password1 = "password";
        _wallet = new Wallet(Words, Password1);
        //for (int i = 0; i < 10; i++)
        //{
        int i = 0;
        var account = _wallet.GetAccount(i);
        Debug.Log("Account index : " + i + " - Address : " + account.Address + " - Private key : " + account.PrivateKey);
        //}
    }

    public Nethereum.Web3.Accounts.Account getAccount()
    {
        return _wallet.GetAccount(0);
    }
}
