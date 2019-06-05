//using System.Collections;
////using System.Collections.Generic;
////using System.Numerics;
////using System.Runtime.InteropServices;
////using Nethereum.ABI.FunctionEncoding.Attributes;
////using Nethereum.ABI.Model;
////using Nethereum.Contracts;
////using Nethereum.Contracts.CQS;
////using Nethereum.Contracts.Extensions;
//using Nethereum.JsonRpc.UnityClient;
//using Nethereum.RPC.Eth.DTOs;
//using Nethereum.Util;
//using UnityEngine;

//public class EtherTransfer : MonoBehaviour {

    
//    // Use this for initialization
//    void Start () {
//        Debug.Log("Transfer Init");
//        StartCoroutine(TransferEther());
//    }


//    //Sample of new features / requests
//    public IEnumerator TransferEther()
//    {
//        var url = "HTTP://127.0.0.1:7545";
//        var privateKey = "750f470f331da664f26b3ca8e05f30b54e21abf0bdbb4706a1ce920c4fd147aa";
//        var account = "0x179496CeA107ee91e8738724CE2931924A65E4eD";
//        //initialising the transaction request sender
//        // string url, string privateKey, string account
//        var ethTransfer = new EthTransferUnityRequest(url, privateKey, account);
        
//        //var receivingAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe";
//        //yield return ethTransfer.TransferEther("0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe", 1.1m, 2);

//        var receivingAddress = "0x7B3150cC598FD65058044Dec34c9db545b8E1E90";
//        yield return ethTransfer.TransferEther("0x7B3150cC598FD65058044Dec34c9db545b8E1E90", 1.1m, 2);

//        if (ethTransfer.Exception != null)
//        {
//            Debug.Log(ethTransfer.Exception.Message);
//            yield break;
//        }

//        var transactionHash = ethTransfer.Result;

//        Debug.Log("Transfer transaction hash:" + transactionHash);

//        //create a poll to get the receipt when mined
//        var transactionReceiptPolling = new TransactionReceiptPollingRequest(url);
//        //checking every 2 seconds for the receipt
//        yield return transactionReceiptPolling.PollForReceipt(transactionHash, 2);
        
//        Debug.Log("Transaction mined");

//        var balanceRequest = new EthGetBalanceUnityRequest(url);
//        yield return balanceRequest.SendRequest(receivingAddress, BlockParameter.CreateLatest());
        
        
//        Debug.Log("Balance of account:" + UnitConversion.Convert.FromWei(balanceRequest.Result.Value));
//    }



//    // Update is called once per frame
//    void Update () {
		
//	}
//}
