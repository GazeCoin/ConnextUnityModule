using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayButton : MonoBehaviour {

    Payment _paymentModule = new Payment();

    public Button btn;

	void Start () {
        Button payButton = btn.GetComponent<Button>();
        payButton.onClick.AddListener(Pay);
	}

    void Pay() {
        string fromPriv = "5d41afd24b7794c5f0dd6d0e7f07d9891d193c5737f74619ad328795ee43e321";
        string fromPub = "0xD2512Bc76c30cEA4E950212D192bf73294054937";
        string toPub = "0x318555976f59537b47838E11d9F085E068714E50";
        StartCoroutine(_paymentModule.MakePayment(
            "HTTP://127.0.0.1:7545", // server
            fromPriv, // from priv
            fromPub, // from pub
            toPub, // to
            1.1m, // send amount
            2 // gas
        ));
    }
}

