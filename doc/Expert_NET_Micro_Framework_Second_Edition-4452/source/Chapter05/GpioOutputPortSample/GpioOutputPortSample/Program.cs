using System;
using System.Threading;
using Microsoft.SPOT.Hardware;

namespace GpioOutputPortSample
{
    public class Program
    {
        public static void Main()
        {
            OutputPort outputPort = new OutputPort(Cpu.Pin.GPIO_Pin0, true);
            while (true)
            {
                Thread.Sleep(500);
                outputPort.Write(!outputPort.Read()); //toggle port
            }
        }
    }
}
