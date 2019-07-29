using System.Collections;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using UnityEngine;

public class Payment
{
    public IEnumerator MakePayment(
        string server, 
        string fromPrivateKey, 
        string fromPublicAddress,
        string toPublicAddress, 
        decimal sendAmount,
        int gas
    ) {
        var ethTransfer = new EthTransferUnityRequest(server, fromPrivateKey);
        yield return ethTransfer.TransferEther(toPublicAddress, sendAmount, gas);
        if (ethTransfer.Exception != null)
        {
            Debug.Log(ethTransfer.Exception.Message);
            yield break;
        }
        var transactionHash = ethTransfer.Result;
        Debug.Log("Transfer transaction hash:" + transactionHash);
        // create a poll to get the receipt when mined
        var transactionReceiptPolling = new TransactionReceiptPollingRequest(server);
        // checking every 2 seconds for the receipt
        yield return transactionReceiptPolling.PollForReceipt(transactionHash, 2);
        Debug.Log("Transaction mined");
        var balanceRequest = new EthGetBalanceUnityRequest(server);
        yield return balanceRequest.SendRequest(toPublicAddress, BlockParameter.CreateLatest());
        Debug.Log("Balance of account:" + UnitConversion.Convert.FromWei(balanceRequest.Result.Value));
    }
}
