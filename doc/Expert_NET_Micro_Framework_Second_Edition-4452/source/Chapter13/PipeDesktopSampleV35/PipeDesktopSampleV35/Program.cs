using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace PipeDesktopSampleV35
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NamedPipeServerStream serverPipeStream = new NamedPipeServerStream("SamplePipe"))
            {
                Console.WriteLine("Waiting for client to connect.");
                serverPipeStream.WaitForConnection();
                Console.WriteLine("Connected to client.");
                byte[] bytes = Encoding.UTF8.GetBytes("Hello World\n");
                while (true)
                {
                    serverPipeStream.Write(bytes, 0, bytes.Length);
                    serverPipeStream.Flush(); //to get sure it is send immediatelly
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
