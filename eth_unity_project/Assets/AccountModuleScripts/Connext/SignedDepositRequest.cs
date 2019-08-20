using System;

public class SignedDepositRequest
{
    public PurchasePayment.PaymentAmounts payment;
    public string sigUser;

	public SignedDepositRequest(PurchasePayment.PaymentAmounts pr)
	{
        payment = pr;
	}

    public void Sign()
    {
        // Convert amounts to byte array.
        Utils.ByteArrayBuilder bab = new Utils.ByteArrayBuilder();
        bab.AddUInt256(payment.getAmountToken());
        bab.AddUInt256(payment.getAmountWei());
        // Now hash it
        //sigUser = HDWallet.HashAndSign(bab.GetByteArray());
    }

}
