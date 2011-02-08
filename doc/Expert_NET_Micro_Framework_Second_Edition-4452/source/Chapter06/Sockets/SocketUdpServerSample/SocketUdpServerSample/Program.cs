using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.SPOT;

namespace SocketUdpServerSample
{
    public class Program
    {
        private const int port = 2000;

        public static void Main()
        {
            using (Socket serverSocket = new Socket(AddressFamily.InterNetwork,
                                                    SocketType.Dgram,
                                                    ProtocolType.Udp))
            {
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
                serverSocket.Bind(remoteEndPoint);
                if (serverSocket.Poll(-1, //timeout in micro seconds
                                           SelectMode.SelectRead))
                {
                    byte[] inBuffer = new byte[serverSocket.Available];
                    int count = serverSocket.ReceiveFrom(inBuffer, ref remoteEndPoint);
                    string message = new string(Encoding.UTF8.GetChars(inBuffer));
                    Debug.Print("Received '" + message + "'.");
                }
            }
        }
    }
}
