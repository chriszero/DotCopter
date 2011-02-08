using System;
using System.Text;
using Microsoft.SPOT;

namespace TextEncodingSample
{
    public class Program
    {
        public static void Main()
        {
            string text = "Hello World";
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            string restoredText = new string(Encoding.UTF8.GetChars(bytes));
            Debug.Print(restoredText);
        }
    }
}
