using System;

public class RequestDeposit
{
    public string depositToken;
    public string depositWei;
    public int lastChanTx;
    public int lastThreadUpdateId;
    public string sigUser;
    private PurchasePayment.PaymentAmounts paymentAmounts;

	public RequestDeposit()
	{
	}

    // Takes PaymentRequest data and sets fields in this class.
    // ready for a request-deposit call.
    public void setPaymentRequest(PurchasePayment.PaymentAmounts pr)
    {
        paymentAmounts = pr;
        depositToken = paymentAmounts.amountToken;
        depositWei = paymentAmounts.amountWei;

        // Sign it 
        SignedDepositRequest sdr = new SignedDepositRequest(pr);
        sdr.sign();
        sigUser = sdr.sigUser;
    }

}
