using System;
using System.Threading;
using Microsoft.SPOT;

namespace SleepSample
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Debug.Print("Hello World!");
                Thread.Sleep(1000); //1000  milliseconds = wait 1 second
            }
        }
    }
}
