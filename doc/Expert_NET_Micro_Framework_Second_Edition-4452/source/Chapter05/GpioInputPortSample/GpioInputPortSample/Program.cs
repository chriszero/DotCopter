using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace GpioInputPortSample
{
    public class Program
    {
        public static void Main()
        {
            InputPort inputPort = new InputPort(Cpu.Pin.GPIO_Pin2, false, Port.ResistorMode.PullDown);
            while (true)
            {
                bool state = inputPort.Read(); //polling of port state
                Debug.Print("GPIO input port at pin " + inputPort.Id + " is " + (state ? "high" : "low"));
                Thread.Sleep(10); //enable device to sleep or emulator to react to Visual Studio
            }
        }
    }
}
