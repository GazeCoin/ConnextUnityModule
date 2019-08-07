using System;

public class RequestDeposit
{
    public string depositToken;
    public string depositWei;
    public uint lastChanTx;
    public uint lastThreadUpdateId;
    public string sigUser;
    private PurchasePayment.PaymentAmounts paymentAmounts;

	public RequestDeposit()
	{
	}

    // Takes PaymentRequest data and sets fields in this class.
    // ready for a request-deposit call.
    public void SetPaymentRequest(PurchasePayment.PaymentAmounts pr)
    {
        paymentAmounts = pr;
        depositToken = paymentAmounts.amountToken;
        depositWei = paymentAmounts.amountWei;

        // Sign it 
        SignedDepositRequest sdr = new SignedDepositRequest(pr);
        sdr.Sign();
        sigUser = sdr.sigUser;
    }

}
