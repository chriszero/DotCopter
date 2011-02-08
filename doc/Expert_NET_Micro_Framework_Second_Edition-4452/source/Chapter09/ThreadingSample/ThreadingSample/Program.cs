using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.SPOT;

namespace ThreadingSample
{
    public class Program
    {
        public static void Main()
        {
            Thread thread = new Thread(MyThreadMethod);
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
            thread.Join();
            Debug.Print("Thread is terminated.");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void MyThreadMethod()
        {
            //do some work here
            int i = 10;
            while (i-- > 0)
            {
                Debug.Print("Hello");
                Thread.Sleep(100);
            }
        }
    }
}
