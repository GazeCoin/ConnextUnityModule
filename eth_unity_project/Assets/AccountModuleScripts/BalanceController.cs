using UnityEngine;

public class BalanceController : MonoBehaviour 
{
    public Balance _balance = new Balance();
    public decimal CurrentBalance = 0;
    public float pollStart = 0;
    public float pollRepeat = 0.1f;
    public string fromPub = "0xD2512Bc76c30cEA4E950212D192bf73294054937";

    public void OnEnable()
    {
        // method name, start time, repeat time
        InvokeRepeating("Poll", pollStart, pollRepeat);
    }

    public void Poll()
    {
        StartCoroutine(_balance.GetBalance(
            "HTTP://127.0.0.1:7545",
            fromPub, (balance) => {
            CurrentBalance = balance;
        }));
    }
}