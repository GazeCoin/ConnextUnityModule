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
        public string amountWei;
        public string amountToken;
        private decimal amountWeiDec;
        private decimal amountTokenDec;

        public void setAmounts(decimal wei, decimal token)
        {
            amountWeiDec = wei;
            amountTokenDec = token;

            this.amountWei = amountWeiDec.ToString();
            this.amountToken = amountTokenDec.ToString();
        }

        public decimal getAmountWei()
        {
            return amountWeiDec;
        }

        public decimal getAmountToken()
        {
            return amountTokenDec;
        }
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