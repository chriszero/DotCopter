using System.IO.Ports;
using System.Text;
using System.Threading;
using Microsoft.SPOT;

namespace SerialPortReadSample
{
    public class Program
    {
        public static void Main()
        {
            SerialPort serialPort = new SerialPort("COM1", 115200, Parity.None);
            serialPort.ReadTimeout = 0;
            serialPort.Open();
            byte[] inBuffer = new byte[32];
            while (true)
            {
                int count = serialPort.Read(inBuffer, 0, inBuffer.Length);
                if (count > 0) // Minimum one byte read
                {
                    char[] chars = Encoding.UTF8.GetChars(inBuffer);
                    string str = new string(chars, 0, count);
                    Debug.Print(str);
                }
                Thread.Sleep(25); // Give device time to sleep
            }
        }
    }
}
