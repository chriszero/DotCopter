using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Cryptography;
using System.Text;

namespace XteaSample
{
    public class Program
    {
        public static void Main()
        {
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; //not null, length 0 - n, 16 bytes used 
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };// only first 8 bytes used, null or less than 8 bytes are padded with zeros at end 

            Key_TinyEncryptionAlgorithm xtea = new Key_TinyEncryptionAlgorithm(key);

            string plainText = "Hello World!"; //original message, min length is 8 bytes
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] cipherBytes = xtea.Encrypt(plainBytes, 0, plainBytes.Length, iv);     //Encryption
            byte[] restoredBytes = xtea.Decrypt(cipherBytes, 0, cipherBytes.Length, iv); //Decryption

            //Output
            Debug.Print("Plain Text    : " + new string(Encoding.UTF8.GetChars(plainBytes)));
            Debug.Print("Restored Text : " + new string(Encoding.UTF8.GetChars(restoredBytes)));
            Debug.Print("Plain Bytes   : " + BufferToString(plainBytes));
            Debug.Print("Cipher Bytes  : " + BufferToString(cipherBytes));
            Debug.Print("Restored Bytes: " + BufferToString(restoredBytes));
        }

        #region diagnostics helpers
        private static string ByteToHex(byte b)
        {
            const string hex = "0123456789ABCDEF";
            int lowNibble = b & 0x0F;
            int highNibble = (b & 0xF0) >> 4;
            string s = new string(new char[] { hex[highNibble], hex[lowNibble] });
            return s;
        }

        private static string BufferToString(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            string s = string.Empty;
            for (int i = 0; i < buffer.Length; i++)
            {
                s += ByteToHex(buffer[i]) + " ";
                if (i > 0 && i % 16 == 0)
                    s += "\n";
            }
            return s;
        }
        #endregion
    }
}
