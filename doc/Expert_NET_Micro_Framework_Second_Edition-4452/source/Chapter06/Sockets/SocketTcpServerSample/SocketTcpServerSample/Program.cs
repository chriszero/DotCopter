using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.SPOT;

namespace SocketTcpServerSample
{
    public class Program
    {
        private const int port = 2000;

        public static void Main()
        {
            using (Socket listeningSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp))
            {
                listeningSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                Debug.Print("Listening for a client...");
                listeningSocket.Listen(1);
                using (Socket communicationSocket = listeningSocket.Accept())
                {
                    Debug.Print("Connected to client.");
                    //wait infinitely to get a response
                    communicationSocket.ReceiveTimeout = -1;
                    byte[] inBuffer = new byte[1000];
                    int count = communicationSocket.Receive(inBuffer);
                    string message = new string(Encoding.UTF8.GetChars(inBuffer));
                    Debug.Print("Received '" + message + "'.");
                }
            }
        }
    }
}
