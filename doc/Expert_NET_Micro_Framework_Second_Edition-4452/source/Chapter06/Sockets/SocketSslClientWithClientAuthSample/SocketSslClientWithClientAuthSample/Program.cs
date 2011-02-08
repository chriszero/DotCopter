using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.Security;

namespace SocketSslClientWithClientAuthSample
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
                using (SslStream sslStream = new SslStream(clientSocket))
                {
                    X509Certificate rootCA =
                             new X509Certificate(Resources.GetBytes(Resources.BinaryResources.MyRootCA));
                    X509Certificate clientCert =
                             new X509Certificate(Resources.GetBytes(Resources.BinaryResources.MyRootCA));
                    sslStream.AuthenticateAsClient("MyServerName", // Hostname needs to match CN of server cert
                                                   clientCert, // Authenticate client
                                                   new X509Certificate[] { rootCA }, // CA certs for verification 
                                                   SslVerification.CertificateRequired, // Verify server
                                                   SslProtocols.Default // Protocols that may be required
                                                   );
                    // Sending
                    byte[] messageBytes = Encoding.UTF8.GetBytes("Hello World!");
                    sslStream.Write(messageBytes, 0, messageBytes.Length);
                }
            }// the socket will be closed here
        }
    }
}
