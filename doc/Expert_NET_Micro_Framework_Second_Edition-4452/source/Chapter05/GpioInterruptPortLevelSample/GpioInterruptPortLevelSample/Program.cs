using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace GpioInterruptPortLevelSample
{
    public class Program
    {
        private static InterruptPort interruptPort;

        public static void Main()
        {
            interruptPort = new InterruptPort(Cpu.Pin.GPIO_Pin2,
                                              false, //no glitch filter 
                                              Port.ResistorMode.PullUp,
                                              Port.InterruptMode.InterruptEdgeLevelLow);
            interruptPort.OnInterrupt += new NativeEventHandler(port_OnInterrupt);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void port_OnInterrupt(uint port, uint state, TimeSpan time)
        {
            Debug.Print("Pin=" + port + " State=" + state + " Time=" + time);
            interruptPort.ClearInterrupt();
        }
    }
}
