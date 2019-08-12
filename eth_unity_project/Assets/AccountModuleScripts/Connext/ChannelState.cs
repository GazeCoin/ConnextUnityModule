using System;
using Nethereum.Web3;
using System.Numerics;
using Nethereum.Contracts;

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
    public uint txCountChain;
    public uint txCountGlobal;
    public string sigHub;
    public string sigUser;

    public decimal getBalanceEthHub()
    {
        if (balanceWeiHub == null) return 0;
        return Web3.Convert.FromWei(BigInteger.Parse(balanceWeiHub));
    }
    public decimal getBalanceEthUser()
    {
        if (balanceWeiUser == null) return 0;
        return Web3.Convert.FromWei(BigInteger.Parse(balanceWeiUser));
    }
    public decimal getBalanceTokenHub()
    {
        if (balanceTokenHub == null) return 0;
        return Web3.Convert.FromWei(BigInteger.Parse(balanceTokenHub));
    }
    public decimal getBalanceTokenUser()
    {
        if (balanceTokenUser == null) return 0;
        return Web3.Convert.FromWei(BigInteger.Parse(balanceTokenUser));
    }

    public ChannelState Clone()
    {
        ChannelState newCS = new ChannelState();
        newCS.contractAddress = contractAddress;
        newCS.user = user;
        newCS.recipient = recipient;
        newCS.balanceWeiHub = balanceWeiHub;
        newCS.balanceWeiUser = balanceWeiUser;
        newCS.balanceTokenHub = balanceTokenHub;
        newCS.balanceTokenUser = balanceTokenUser;
        newCS.pendingDepositWeiHub = pendingDepositWeiHub;
        newCS.pendingDepositWeiUser = pendingDepositWeiUser;
        newCS.pendingDepositTokenHub = pendingDepositTokenHub;
        newCS.pendingDepositTokenUser = pendingDepositTokenUser;
        newCS.pendingWithdrawalWeiHub = pendingWithdrawalWeiHub;
        newCS.pendingWithdrawalWeiUser = pendingWithdrawalWeiUser;
        newCS.pendingWithdrawalTokenHub = pendingWithdrawalTokenHub;
        newCS.pendingWithdrawalTokenUser = pendingWithdrawalTokenUser;
        newCS.threadCount = threadCount;
        newCS.threadRoot = threadRoot;
        newCS.timeout = timeout;
        newCS.txCountChain = txCountChain;
        newCS.txCountGlobal = txCountGlobal;
        newCS.sigHub = sigHub;
        newCS.sigUser = sigUser;
        return newCS;
    }

    public byte[] getBytes()
    {
        Utils.ByteArrayBuilder bab = new Utils.ByteArrayBuilder();
        bab.AddAddress(contractAddress);
        bab.AddAddress(user);
        bab.AddAddress(recipient);
        bab.AddUInt256(balanceWeiHub);
        bab.AddUInt256(balanceWeiUser);
        bab.AddUInt256(balanceTokenHub);
        bab.AddUInt256(balanceTokenUser);
        bab.AddUInt256(pendingDepositWeiHub);
        bab.AddUInt256(pendingWithdrawalWeiHub);
        bab.AddUInt256(pendingDepositWeiUser);
        bab.AddUInt256(pendingWithdrawalWeiUser);
        bab.AddUInt256(pendingDepositTokenHub);
        bab.AddUInt256(pendingWithdrawalTokenHub);
        bab.AddUInt256(pendingDepositTokenUser);
        bab.AddUInt256(pendingWithdrawalTokenUser);
        bab.AddUInt256(txCountGlobal);
        bab.AddUInt256(txCountChain);
        bab.AddBytes32(threadRoot);
        bab.AddUInt256(threadCount);
        bab.AddUInt256(timeout);

        return bab.GetByteArray();
    }

    public FunctionMessage UserAuthorizedUpdateFunction()
    {
        return new ChannelManagerContract.UserAuthorizedUpdateFunction()
        {
            Recipient       = recipient,
            WeiBalances     = new[] {
                BigInteger.Parse(balanceWeiHub),
                BigInteger.Parse(balanceWeiUser)
            },
            TokenBalances   = new[] {
                BigInteger.Parse(balanceTokenHub),
                BigInteger.Parse(balanceTokenUser)
            },
            PendingWeiUpdates = new[] {
                BigInteger.Parse(pendingDepositWeiHub),
                BigInteger.Parse(pendingWithdrawalWeiHub),
                BigInteger.Parse(pendingDepositWeiUser),
                BigInteger.Parse(pendingWithdrawalWeiUser), },
            PendingTokenUpdates = new[] {
                BigInteger.Parse(pendingDepositTokenHub),
                BigInteger.Parse(pendingWithdrawalTokenHub),
                BigInteger.Parse(pendingDepositTokenUser),
                BigInteger.Parse(pendingWithdrawalTokenUser), },
            TxCount         = new[] {
                new BigInteger(txCountGlobal),
                new BigInteger(txCountChain)},
            ThreadRoot      = Utils.StringToBytes32(this.threadRoot),
            ThreadCount     = BigInteger.Parse(this.threadCount),
            Timeout         = BigInteger.Parse(this.timeout),
            SigHub          = sigHub
        };
    }
}

