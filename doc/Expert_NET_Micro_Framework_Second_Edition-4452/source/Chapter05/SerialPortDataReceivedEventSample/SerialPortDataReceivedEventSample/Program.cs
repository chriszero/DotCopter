using System.IO.Ports;
using System.Threading;
using Microsoft.SPOT;

namespace SerialPortDataReceivedEventSample
{
    public class Program
    {
        private static SerialPort serialPort;

        public static void Main()
        {
            serialPort = new SerialPort("COM1", 115200, Parity.None);
            serialPort.Open();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.Print("Number of Bytes to read=" + serialPort.BytesToRead);
        }
    }
}
