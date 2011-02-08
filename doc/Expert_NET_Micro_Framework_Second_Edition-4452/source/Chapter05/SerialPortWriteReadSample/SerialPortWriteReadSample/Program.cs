using System.IO.Ports;
using System.Text;
using Microsoft.SPOT;

namespace SerialPortWriteReadSample
{
    public class Program
    {
        public static void Main()
        {
            SerialPort serialPort = new SerialPort("COM1", 115200, Parity.None);
            serialPort.ReadTimeout = 5000; // milliseconds
            serialPort.Open();
            byte[] outBuffer = Encoding.UTF8.GetBytes("All right?\r\n");
            byte[] inBuffer = new byte[2];
            while (true)
            {
                Debug.Print("Request data");
                serialPort.Write(outBuffer, 0, outBuffer.Length);
                int count = serialPort.Read(inBuffer, 0, 2);
                if (count == 2)
                {
                    Debug.Print("Received expected two bytes!");
                }
                else
                {
                    if (count == 0)
                        Debug.Print("No response!");
                    if (count == 1)
                        Debug.Print("Not enough bytes received!");
                }
                Debug.Print(string.Empty);
            }
        }
    }
}
