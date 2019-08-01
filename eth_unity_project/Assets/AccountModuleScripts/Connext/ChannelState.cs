﻿using System;
using Nethereum.Web3;
using System.Numerics;

public class ChannelState
{
    public string contractAddress;
    public string user;
    public string recipient;
    public string balanceWeiHub;
    public string balanceWeiUser;
    public string balanceTokenHub;
    public string balanceTokenUser;
    public string pendingDepositWeiHub;
    public string pendingDepositWeiUser;
    public string pendingDepositTokenHub;
    public string pendingDepositTokenUser;
    public string pendingWithdrawalWeiHub;
    public string pendingWithdrawalWeiUser;
    public string pendingWithdrawalTokenHub;
    public string pendingWithdrawalTokenUser;
    public string threadCount;
    public string threadRoot;
    public string timeout;
    public int txCountChain;
    public int txCountGlobal;
    public string sigHub;
    public string sigUser;

    public decimal getBalanceEthHub()
    {
        return Web3.Convert.FromWei(BigInteger.Parse(balanceWeiHub));
    }
    public decimal getBalanceEthUser()
    {
        return Web3.Convert.FromWei(BigInteger.Parse(balanceWeiUser));
    }
    public decimal getBalanceTokenHub()
    {
        return Web3.Convert.FromWei(BigInteger.Parse(balanceTokenHub));
    }
    public decimal getBalanceTokenUser()
    {
        return Web3.Convert.FromWei(BigInteger.Parse(balanceTokenUser));
    }
}

