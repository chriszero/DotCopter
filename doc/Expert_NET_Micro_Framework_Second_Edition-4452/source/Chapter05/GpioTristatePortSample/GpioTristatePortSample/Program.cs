using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace GpioTristatePortSample
{
    public class Program
    {
        public static void Main()
        {
            TristatePort tristatePort = new TristatePort(Cpu.Pin.GPIO_Pin4,
                                                         false, //initial state
                                                         false, //no glitch filter
                                                         Port.ResistorMode.PullUp);

            Debug.Print("Port is inactive and acts as input port.");
            Debug.Print("Input = " + tristatePort.Read());

            tristatePort.Active = true;
            Debug.Print("Port is active and acts as output port.");
            tristatePort.Write(true);
        }
    }
}
