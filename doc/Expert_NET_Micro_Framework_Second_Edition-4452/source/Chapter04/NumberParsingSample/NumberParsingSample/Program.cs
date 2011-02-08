using System;
using Microsoft.SPOT;
using Kuehner;

namespace NumberParsingSample
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print(NumberParser.ParseInt64("1234").ToString());
            Debug.Print(NumberParser.ParseInt64("-1234").ToString());
            Debug.Print(NumberParser.ParseUInt64("1234").ToString());
            Debug.Print(NumberParser.ParseUInt64Hex("FF").ToString());
            Debug.Print(NumberParser.ParseDouble("1234.56").ToString());
            Debug.Print(NumberParser.ParseDouble("-1234.56").ToString());
            Debug.Print(NumberParser.ParseDouble("+1234.56").ToString());
            Debug.Print(NumberParser.ParseDouble("1,234.56").ToString());
            Debug.Print(NumberParser.ParseDouble("1.23e2").ToString());
            Debug.Print(NumberParser.ParseDouble("1.23e-2").ToString());
            Debug.Print(NumberParser.ParseDouble("123e+2").ToString());
            double result;
            if (NumberParser.TryParseDouble("1234.56a", out result))
                Debug.Print(result.ToString());
            else
                Debug.Print("1234.56a is not a valid number.");

        }
    }
}
