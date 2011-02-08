using System;
using System.Threading;
using Microsoft.SPOT;

namespace ThreadingEventSample
{
    public class Program
    {
        private static AutoResetEvent ev = new AutoResetEvent(false);

        static void Main()
        {
            Thread thr = new Thread(WaitForEvent);
            thr.Start();

            Debug.Print("Waiting...");
            ev.WaitOne(); //waiting for notification
            Debug.Print("Notified");
        }

        private static void WaitForEvent()
        {
            Thread.Sleep(1000); //sleep to simulate doing something
            ev.Set(); //wake up other thread
        }
    }
}
