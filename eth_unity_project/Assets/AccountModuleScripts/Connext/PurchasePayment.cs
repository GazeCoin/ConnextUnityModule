using System;
using Nethereum.Web3;
using System.Numerics;

public class PurchasePayment
{
    public Metadata meta = new Metadata();
    public string recipient; // address
    public PaymentAmounts amount; // {wei, tokens}
    public string type = "PT_CHANNEL";
    public Update update = new Update();

    public class PaymentAmounts
    {
        public decimal amountWei;
        public decimal amountToken;
    }

    public class Metadata
    {
        public string purchaseId = "payment";
    }

    public class Update
    {
        public Args args = new Args();
        public string reason = "Payment";
        public string sigUser;
        public int txCount;
    }

    public class Args
    {
        public string recipient;
        public decimal amountWei;
        public decimal amountToken;
    }
}