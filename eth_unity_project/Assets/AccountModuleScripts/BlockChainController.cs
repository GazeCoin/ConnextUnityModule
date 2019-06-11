using UnityEngine;

public class BlockChainController : MonoBehaviour {

    public delegate void BalanceEvent(string currentBalance);
    public static event BalanceEvent DispatchUpdateBalanceEvent;

    void Start()
    {

        Payment _payment = new Payment();
        Balance _balance = new Balance();
        Account _account = new Account();

        string fromPriv = "5d41afd24b7794c5f0dd6d0e7f07d9891d193c5737f74619ad328795ee43e321";
        string fromPub = "0xD2512Bc76c30cEA4E950212D192bf73294054937";
        string toPub = "0x318555976f59537b47838E11d9F085E068714E50";

        //// Payment example
        StartCoroutine(_payment.MakePayment(
            "HTTP://127.0.0.1:7545", // server
            fromPriv, // from priv
            fromPub, // from pub
            toPub, // to
            1.1m, // send amount
            2 // gas
        )); // DispatchUpdateBalanceEvent(balance + " ETH");

        //StartCoroutine(_payment.MakePayment(
        //    "HTTP://127.0.0.1:7545", // server
        //    fromPriv, // from priv
        //    fromPub, // from pub
        //    toPub, // to
        //    1.1m, // send amount
        //    2 // gas
        //), (balance) => {
        //    DispatchUpdateBalanceEvent(balance); 
        //}); 

        // Check balance > If balance is greater than 0 > make payment > broadcast balance update
        // StartCoroutine(_balance.GetBalance(
        //    "HTTP://127.0.0.1:7545",
        //    fromPub, (balance) => {
        //    // DispatchUpdateBalanceEvent(balance + " ETH");
        //    if (balance - X >= 0) {
        //     StartCoroutine(_payment.MakePayment(
        //       "HTTP://127.0.0.1:7545", // server
        //       fromPriv, // from priv
        //       fromPub, // from pub
        //       toPub, // to
        //       1.1m, // send amount
        //       2 // gas
        //     ));
        //   }
        // }));

        //// Balance example
        StartCoroutine(_balance.GetBalance(
            "HTTP://127.0.0.1:7545", 
            fromPub, (balance) => {
            DispatchUpdateBalanceEvent(balance + " ETH");
        }));

        //// Create account example
        //_account.CreateAccount("strong_password", (address, encryptedJson) => {
        //    Debug.Log(address);
        //    Debug.Log(encryptedJson);
        //});

	}
}
