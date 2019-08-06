using System;
using System.Globalization;
using System.Numerics;

public class Utils
{
	public Utils()
	{
	}

    public static byte[] AddressToBytes(string address)
    {
        if (!address.StartsWith("0x") || address.Length != 42)
        {
            throw new Exception("Expected address as string beginning with 0x");
        }
        // Convert string to hex, as bytes.
        // Address = 160 bits = 20 bytes.
        NumberStyles style = NumberStyles.AllowHexSpecifier;
        byte[] bytes = new byte[20];
        int i = 0;
        for (int pos = 2; pos < address.Length; pos += 2)
        {
            bytes[i++] = Byte.Parse(address.Substring(pos, 2), style);
        }
        return bytes;
    }
    public static byte[] StringToBytes32(string hexString)
    {
        byte[] bytes = new byte[32];
        if (hexString == null) return bytes;
        if (!hexString.StartsWith("0x"))
        {
            throw new Exception("Expected string beginning with 0x");
        }
        // Convert string to hex, as bytes.
        NumberStyles style = NumberStyles.AllowHexSpecifier;
        int i = 0;
        for (int pos = 2; pos < hexString.Length; pos += 2)
        {
            bytes[i++] = Byte.Parse(hexString.Substring(pos, 2), style);
        }
        return bytes;
    }

    public static byte[] UInt256ToBytes(decimal num)
    {
        // decimal = 16 bytes. uint256 = 32 bytes.
        // Set most significant bytes to 0
        Int32[] bits = decimal.GetBits(num); // 4 x 4 bytes little-endian
        // TODO: check that 1st 2 bytes are 0 (sign, exponent)
        byte[] bytes32 = new byte[32]; // 32 bytes = 256 bits
        Array.Clear(bytes32, 0, 32);
        for (int i = 0; i < 4; i++)
        {
            byte[] intBits = BitConverter.GetBytes(bits[i]); // Each int32 => 4 x 8-bit bytes, big-endian
            Array.Reverse(intBits);
            Array.Copy(intBits, 0, bytes32, (3 - i) * 4 + 16, 4); // Copy all 4 bytes into reverse positions
        }
        return bytes32;
    }

    public class ByteArrayBuilder
    {
        public int nextPosition = 0;
        public byte[] byteArray = new byte[0];
        
        public ByteArrayBuilder()
        {
        }

        public byte[] GetByteArray()
        {
            return byteArray;
        }

        private void InsertBytes(byte[] bytes)
        {
            Array.Resize(ref byteArray, nextPosition + bytes.Length);
            Array.Copy(bytes, 0, byteArray, nextPosition, bytes.Length);
            nextPosition += bytes.Length;
        }

        public void AddAddress(string address)
        {
            byte[] bytes = AddressToBytes(address);
            InsertBytes(bytes);
        }

        public void AddUInt256(decimal num)
        {
            byte[] bytes = UInt256ToBytes(num);
            InsertBytes(bytes);
        }
        public void AddUInt256(string num)
        {
            AddUInt256(Decimal.Parse(num));
        }

        public void AddBytes32(string hexString)
        {
            byte[] bytes = StringToBytes32(hexString);
            InsertBytes(bytes);
        }

    }

}
