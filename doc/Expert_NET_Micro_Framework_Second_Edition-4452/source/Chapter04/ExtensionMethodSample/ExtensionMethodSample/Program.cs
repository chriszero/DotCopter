using System;
using Microsoft.SPOT;
using MyExtensions;

namespace ExtensionMethodSample
{
    public class Program
    {
        public static void Main()
        {
            string s = "Hello world.";
            bool b = s.Contains("world");
            Debug.Print("Contains=" + b);
        }
    }
}
