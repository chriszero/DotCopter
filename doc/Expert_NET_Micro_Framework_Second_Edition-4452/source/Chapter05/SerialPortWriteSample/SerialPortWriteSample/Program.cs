using System.IO.Ports;
using System.Text;
using System.Threading;

namespace SerialPortWriteSample
{
    public class Program
    {
        public static void Main()
        {
            SerialPort serialPort = new SerialPort("COM1", 9600);
            serialPort.Open();
            byte[] outBuffer = Encoding.UTF8.GetBytes("Hello World!\r\n");
            int i = 10;
            while (i-- > 0)
            {
                serialPort.Write(outBuffer, 0, outBuffer.Length);
                Thread.Sleep(500);
            }
            serialPort.Close();
            // Keep the emulator running to see results
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
