using System.IO.Ports;
using Microsoft.SPOT.Hardware;

namespace HardwareProviderSample
{
    public class Program
    {
        public static void Main()
        {
            HardwareProvider.Register(new MyHardwareProvider());

            SerialPort serialPortA = new SerialPort("COM1");
            serialPortA.Open(); //reserves Pin 0,1,2 and 3 for COM1
            OutputPort outputPort = new OutputPort(Cpu.Pin.GPIO_Pin1, false); // will fail
            Port.ReservePin(Cpu.Pin.GPIO_Pin1, true);
        }
    }
}
