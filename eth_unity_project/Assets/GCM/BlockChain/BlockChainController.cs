using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
        account = gazeAPI.Account;
        if (account != null)
        {
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
            balanceText.text = "Balance: " + gazeAPI.GetBalance("ETH") + " ETH/" + gazeAPI.GetBalance("DAI") + " DAI";
        }
    }

    public static BlockChainController getInstance()
    {
        return bcc;
    }

    public void PayButton_OnClick()
    {
        RequestDeposit();
    }

}
