using System.Collections;
//using Nethereum.JsonRpc.UnityClient;
using UnityEngine;

public class Balance {
    const int REPEAT_DURATION = 30; // seconds

    //public IEnumerator GetBalance(string server, string address, System.Action<decimal> callback)
    //{
    //    //var getBalanceRequest = new EthGetBalanceUnityRequest(server);
    //    //yield return getBalanceRequest.SendRequest(address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());
    //    //if (getBalanceRequest.Exception == null)
    //    //{
    //    //    var balance = getBalanceRequest.Result.Value;
    //    //    callback(Nethereum.Util.UnitConversion.Convert.FromWei(balance, 18));
    //    //}
    //    //else
    //    //{
    //    //    throw new System.InvalidOperationException("Get balance request failed " + getBalanceRequest.Exception.Message);
    //    //}
    //}

    public IEnumerator PeriodicBalanceRequest (string server, string address, System.Action<decimal> callback)
    {
        do
        {
            yield return new WaitForSeconds(REPEAT_DURATION);
           // yield return GetBalance(server, address, callback);
        } while (true);
    }
}