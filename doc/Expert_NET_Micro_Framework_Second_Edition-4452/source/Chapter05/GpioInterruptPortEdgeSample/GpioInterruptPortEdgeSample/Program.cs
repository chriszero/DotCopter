using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace GpioInterruptPortEdgeSample
{
    public class Program
    {
        public static void Main()
        {
            InterruptPort port = new InterruptPort(Cpu.Pin.GPIO_Pin3,
                                                   false, //no glitch filter 
                                                   Port.ResistorMode.PullDown,
                                                   Port.InterruptMode.InterruptEdgeBoth);
            port.OnInterrupt += new NativeEventHandler(port_OnInterrupt);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void port_OnInterrupt(uint port, uint state, TimeSpan time)
        {
            Debug.Print("Pin=" + port + " State=" + state + " Time=" + time);
        }
    }
}
