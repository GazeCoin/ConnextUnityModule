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
    private string ethNodeUrl = "https://rpc.gazecoin.xyz";
    private static BlockChainController bcc;
    ConnextClient connext;
    private static HDWallet wallet;
    private decimal ethBalance;
    private decimal tokenBalance;

    void Start () {
        bcc = this;

        Balance _balance = new Balance();
        //Account _account = new Account();
        wallet = new HDWallet();

        wallet.createWallet();
        account = HDWallet.getAccount();

        AccountText.text = "Address: " + account.Address.ToString();

        // Balance example
        StartCoroutine(_balance.PeriodicBalanceRequest(
            ethNodeUrl,
            account.Address, (balance) => {
                //balanceText.text = "Balance: " + balance;
                ethBalance = balance;
                Debug.Log("L1 balance is " + balance);
        }));

        connext = new ConnextClient();
        connext.Init();
    }

    async void RequestDeposit()
    {
        await connext.RequestDeposit(0, Nethereum.Web3.Web3.Convert.ToWei(0.01));
    }

    void OnPreRender()
    {
        if (connext.getChannelState() != null)
        {
            balanceText.text = "Balance: " + connext.getChannelState().getBalanceTokenUser() + " GZE";
        }
        if (account.Address != null)
        {
            //var txt = "Addr: " + account.Address.ToString().Substring(2);
            //AccountText.text = txt;
            //Debug.Log("pre render" + txt);
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

    public static HDWallet getWallet()
    {
        return wallet;
    }
}
