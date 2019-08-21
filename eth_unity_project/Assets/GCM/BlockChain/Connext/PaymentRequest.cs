using System;

// Top-level object for a payment request.
public class PaymentRequest
{
    public Metadata meta = new Metadata();
    public PurchasePayment[] payments = new PurchasePayment[0];

	public PaymentRequest()
	{
	}

    public class Metadata
    {
        public string purchaseId = "payment";
    }
}
