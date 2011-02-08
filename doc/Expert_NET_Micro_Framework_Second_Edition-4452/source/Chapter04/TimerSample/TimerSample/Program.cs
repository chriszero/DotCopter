using System;
using System.Threading;
using Microsoft.SPOT;

namespace TimerSample
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print("Start");
            System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(OnTimer), null, 0, 500);
            Thread.Sleep(Timeout.Infinite);
        }

        private static void OnTimer(object state)
        {
            Debug.Print("Timer");
        }
    }
}
