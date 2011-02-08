using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace HardwareUtilitySample
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Debug.Print(Utility.GetLastBootTime().ToString());
                Debug.Print(Utility.GetMachineTime().ToString());
                Debug.Print(string.Empty);
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
