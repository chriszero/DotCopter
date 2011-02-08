using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.SPOT;

namespace SocketTcpClientSample
{
    public class Program
    {
        private const string dottedServerIPAddress = "127.0.0.1";
        private const int port = 2000;

        public static void Main()
        {
            using (Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                                                    SocketType.Stream,
                                                    ProtocolType.Tcp))
            {
                // Addressing
                IPAddress ipAddress = IPAddress.Parse(dottedServerIPAddress);
                IPEndPoint serverEndPoint = new IPEndPoint(ipAddress, port);
                // Connecting
                Debug.Print("Connecting to server " + serverEndPoint + ".");
                clientSocket.Connect(serverEndPoint);
                Debug.Print("Connected to server.");
                // Sending
                byte[] messageBytes = Encoding.UTF8.GetBytes("Hello World!");
                clientSocket.Send(messageBytes);
            }// the socket will be closed here
        }
    }
}
