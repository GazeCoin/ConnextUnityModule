using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Nethereum.Web3;

public class BlockChainController : MonoBehaviour {

    public Text AccountText;
    public Text balanceText;
    public delegate void ButtonClick();
    public Nethereum.Web3.Accounts.Account account;
    private static BlockChainController bcc;
    private decimal ethBalance;
    private decimal tokenBalance;
    private GazeCoinEthAPI gazeAPI;
    private GazeCoinEthAPI.BoolEvent initCompleteEvent;

    void Start () {
        bcc = this;
        Balance _balance = new Balance();

        gazeAPI = new GazeCoinEthAPI();
        gazeAPI.InitComplete.AddListener(InitComplete);
        gazeAPI.Init();
        // Start listeners

    }

    void InitComplete(bool success)
    {
        Debug.Log("Init complete " + success);
        if (success)
        {
            account = gazeAPI.Account;
            AccountText.text = "Address: " + account.Address.ToString();
        }
    }

    async void RequestDeposit()
    {
        await gazeAPI.RequestDeposit(0.005m);
    }

    void OnPreRender()
    {
        
        if (gazeAPI.IsReady())
        {
            balanceText.text = "Balance: " + gazeAPI.GetBalance("DAI") + " DAI";
        }
    }

    public static BlockChainController getInstance()
    {
        return bcc;
    }

    public void PayButton_OnClick()
    {
        /*
        Debug.Log("Pay onClick");
        Payment _payment = new Payment();
        // Payment example
        StartCoroutine(_payment.MakePayment(
           ethNodeUrl, // server
           "750f470f331da664f26b3ca8e05f30b54e21abf0bdbb4706a1ce920c4fd147aa", // from priv
           account.Address, // from pub
           "0x7B3150cC598FD65058044Dec34c9db545b8E1E90", // to
           1.1m, // send amount
           2 // gas
        ));
    */
        RequestDeposit();
    }

}
