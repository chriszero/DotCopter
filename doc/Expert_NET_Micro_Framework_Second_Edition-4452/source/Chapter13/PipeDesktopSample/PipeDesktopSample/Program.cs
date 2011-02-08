using System;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Threading;

namespace PipeDesktopSample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string pipeName = @"\\.\pipe\SamplePipe";
            const int inputBufferSize = 4096;
            const int outputBufferSize = 4096;

            IntPtr hPipe = NativeMethods.CreateNamedPipe(
                                     pipeName,
                                     NativeMethods.PIPE_ACCESS_DUPLEX, // read/write access 
                                     NativeMethods.PIPE_TYPE_BYTE | // message type pipe 
                                     NativeMethods.PIPE_READMODE_BYTE | // message-read mode 
                                     NativeMethods.PIPE_WAIT, // blocking mode 
                                     NativeMethods.PIPE_UNLIMITED_INSTANCES, // max. instances 
                                     outputBufferSize, // output buffer size 
                                     inputBufferSize, // input buffer size 
                                     0, // default client time-out for WaitNamePipe Method
                                     IntPtr.Zero); // no security attribute 
            SafeFileHandle safeHandle = new SafeFileHandle(hPipe, true);
            if (safeHandle.IsInvalid)
                throw new IOException("Could not open pipe " + pipeName + ".");

            Console.WriteLine("Waiting for client to connect.");
            bool connected = NativeMethods.ConnectNamedPipe(hPipe, IntPtr.Zero) ||
                             Marshal.GetLastWin32Error() == NativeMethods.ERROR_PIPE_CONNECTED;
            if (!connected)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            Console.WriteLine("Connected to client.");

            Stream stream = new FileStream(safeHandle, FileAccess.ReadWrite, 4096);
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Hello World\n");
                while (true)
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();//important to send immediately
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                NativeMethods.DisconnectNamedPipe(safeHandle.DangerousGetHandle()); //proper disconnect before closing handle
                stream.Dispose();
            }
        }
    }
}
