using System;
using System.Text;
using Microsoft.SPOT.Cryptography;

namespace XteaCryptographyDesktopSample
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; //not null, length 0 - n, 16 bytes used 
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };// only first 8 bytes used, null or less than 8 bytes are padded with zeros at end 

            Key_TinyEncryptionAlgorithm xtea = new Key_TinyEncryptionAlgorithm(key);

            string plainText = "Hello World!"; //original message, min length is 8 bytes
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] cipherBytes = xtea.Encrypt(plainBytes, 0, plainBytes.Length, iv);     //Encryption
            byte[] restoredBytes = xtea.Decrypt(cipherBytes, 0, cipherBytes.Length, iv); //Decryption

            //Output
            Console.WriteLine("Plain Text    : " + new string(Encoding.UTF8.GetChars(plainBytes)));
            Console.WriteLine("Restored Text : " + new string(Encoding.UTF8.GetChars(restoredBytes)));
            Console.WriteLine("Plain Bytes   : " + BufferToString(plainBytes));
            Console.WriteLine("Cipher Bytes  : " + BufferToString(cipherBytes));
            Console.WriteLine("Restored Bytes: " + BufferToString(restoredBytes));
            Console.ReadKey(true);
        }

        #region diagnostics helpers
        private static string BufferToString(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            string s = string.Empty;
            foreach (byte b in buffer)
            {
                if (s.Length > 0)
                    s += " ";
                s += b.ToString("X2");
            }
            return s;
        }
        #endregion
    }
}
