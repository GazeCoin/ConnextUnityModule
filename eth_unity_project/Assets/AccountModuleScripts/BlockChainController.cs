using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockChainController : MonoBehaviour {

    public Text AccountText;
    public Nethereum.Web3.Accounts.Account account;

	void Start () {

        Payment _payment = new Payment();
        Balance _balance = new Balance();
        //Account _account = new Account();
        HDWallet _wallet = new HDWallet();

        _wallet.createWallet();
        account = _wallet.getAccount();

        AccountText.text = "Address: " + account.Address.ToString();

        // Payment example
        StartCoroutine(_payment.MakePayment(
           "HTTP://127.0.0.1:7545", // server
           "750f470f331da664f26b3ca8e05f30b54e21abf0bdbb4706a1ce920c4fd147aa", // from priv
           "0x179496CeA107ee91e8738724CE2931924A65E4eD", // from pub
           "0x7B3150cC598FD65058044Dec34c9db545b8E1E90", // to
           1.1m, // send amount
           2 // gas
        ));

        // Balance example
        StartCoroutine(_balance.GetBalance(
            "HTTP://127.0.0.1:7545", 
            "0x179496CeA107ee91e8738724CE2931924A65E4eD", (balance) => {
            Debug.Log("Balance is " + balance);
        }));

        // Create account example
        //_account.CreateAccount("strong_password", (address, encryptedJson) => {
        //    Debug.Log(address);
        //    Debug.Log(encryptedJson);
        //});

	}

    void OnPreRender()
    {
        if (account.Address != null)
        {
            var txt = "Addr: " + account.Address.ToString();
            AccountText.text = txt;
        }
        Debug.Log("pre render" + account.Address.ToString());
    }

}
