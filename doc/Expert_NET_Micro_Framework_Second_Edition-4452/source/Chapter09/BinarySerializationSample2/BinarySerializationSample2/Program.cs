using System;
using Microsoft.SPOT;

namespace BinarySerializationSample2
{
    public class Program
    {
        public static void Main()
        {
            MySerializableClass2 original = new MySerializableClass2();
            Debug.Print("original: " + original.ToString());
            byte[] buffer = Reflection.Serialize(original, typeof(MySerializableClass2));
            MySerializableClass2 restored = (MySerializableClass2)Reflection.Deserialize(buffer, typeof(MySerializableClass2));
            Debug.Print("restored: " + restored.ToString());
            Debug.Print("Number of bytes: " + buffer.Length.ToString());
            Debug.Print(BufferToString(buffer));
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