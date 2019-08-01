using System;

public class RequestDeposit
{
    public decimal depositToken;
    public decimal depositWei;
    public int lastChanTx;
    public int lastThreadUpdateId;
    public string sigUser;
    private PaymentRequest paymentRequest;

	public RequestDeposit()
	{
	}

    // Takes PaymentRequest data and sets fields in this class.
    // ready for a request-deposit call.
    public void setPaymentRequest(PaymentRequest pr)
    {
        paymentRequest = pr;
        var amount = pr.payments[0].amount;
        depositToken = amount.amountToken;
        depositWei = amount.amountWei;

        // Sign it 
        SignedDepositRequest sdr = new SignedDepositRequest(pr);
        sdr.sign();
        sigUser = sdr.sigUser;
    }

}
