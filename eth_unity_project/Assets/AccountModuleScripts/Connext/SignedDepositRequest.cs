using System;

public class SignedDepositRequest
{
    public PurchasePayment.PaymentAmounts payment;
    public string sigUser;

	public SignedDepositRequest(PurchasePayment.PaymentAmounts pr)
	{
        payment = pr;
	}

    public void sign()
    {
        // Convert amounts to byte array.
        byte[] token = DecimalToBytes(payment.amountToken);
        byte[] wei = DecimalToBytes(payment.amountWei);
        // Put into a single array
        byte[] hashBytes = new byte[64];
        Array.Copy(token, 0, hashBytes, 0, 32); // 0-31
        Array.Copy(wei, 0, hashBytes, 32, 32); // 32-63
        // Now hash it
        sigUser = HDWallet.sign(hashBytes);
    }

    // Return byte[32] representation of a decimal
    public static byte[] DecimalToBytes(decimal dec)
    {
        // decimal = 16 bytes. uint256 = 32 bytes.
        // Set most significant bytes to 0
        Int32[] bits = decimal.GetBits(dec); // 4 x 32-bit little-endian
        // TODO: check that 1st 2 bytes are 0 (sign, exponent)
        byte[] bytes32 = new byte[32];
        Array.Clear(bytes32, 0, 16);
        for (int i = 4; i < 8; i++)
        {
            byte[] intBits = BitConverter.GetBytes(bits[7 - i]); // Each int32 => 4 x 8-bit bytes, big-endian
            Array.Copy(intBits, 0, bytes32, i * 4 + 16, 4); // Copy all 4 to 
        }
        return bytes32;
    }

}
