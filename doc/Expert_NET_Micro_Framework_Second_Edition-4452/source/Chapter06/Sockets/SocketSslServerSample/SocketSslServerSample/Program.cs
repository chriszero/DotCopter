using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.Security;

namespace SocketSslServerSample
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
                    using (SslStream sslStream = new SslStream(communicationSocket))
                    {
                        X509Certificate serverCert =
                             new X509Certificate(Resources.GetBytes(Resources.BinaryResources.MyServer));
                        sslStream.AuthenticateAsServer(serverCert, // To authenticate the server 
                                                       new X509Certificate[] {},       // No CA certificates
                                                       SslVerification.NoVerification, // Do not verify client
                                                       SslProtocols.Default // Protocols that may be used
                                                       );
                        //wait infinitely to get a response
                        sslStream.ReadTimeout = -1;
                        byte[] inBuffer = new byte[1000];
                        int count = sslStream.Read(inBuffer, 0, inBuffer.Length);
                        string message = new string(Encoding.UTF8.GetChars(inBuffer));
                        Debug.Print("Received '" + message + "'.");
                    }
                }
            }
        }
    }
}
