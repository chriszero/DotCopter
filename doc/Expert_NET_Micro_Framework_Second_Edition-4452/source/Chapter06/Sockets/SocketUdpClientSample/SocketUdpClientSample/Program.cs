using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketUdpClientSample
{
    public class Program
    {
        private const string dottedServerIPAddress = "127.0.0.1";
        //use 255.255.255.255 for a broadcast
        private const int port = 2000;

        public static void Main()
        {
            using (Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                                                    SocketType.Dgram,
                                                    ProtocolType.Udp))
            {
                // Addressing
                IPAddress ipAddress = IPAddress.Parse(dottedServerIPAddress);
                IPEndPoint serverEndPoint = new IPEndPoint(ipAddress, port);

                // Sending
                byte[] messageBytes = Encoding.UTF8.GetBytes("Hello World!");
                clientSocket.SendTo(messageBytes, serverEndPoint);
            }// the socket will be closed here
        }
    }
}
