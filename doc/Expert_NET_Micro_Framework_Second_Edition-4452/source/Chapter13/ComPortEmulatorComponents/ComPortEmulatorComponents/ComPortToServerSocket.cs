using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Microsoft.SPOT.Emulator.Com;

namespace Kuehner.SPOT.Emulator
{
    public class ComPortToServerSocket : ComPortToStream
    {
        private Socket serverSocket;
        private IPAddress clientIpAddress = IPAddress.Loopback;
        private int ipPort = 23;
        private bool showWaitMessage = true;
        
        protected override void InitializeProtected()
        {
            base.InitializeProtected();
            if (this.Stream == null)
            {
                this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.serverSocket.Bind(new IPEndPoint(this.clientIpAddress, this.ipPort));
                this.serverSocket.Listen(1);
                string message = string.Format(null, "The component {0} is waiting for a client to connect at port {1} for COM{2}.",
                                               this.ComponentId, this.ipPort, this.ComPortHandle.PortNumber);
                System.Diagnostics.Trace.WriteLine(message);
                if (this.showWaitMessage)
                {
                    MessageBox.Show(message,
                                    "ComPortToSocketServer Component waiting at Port " + this.ipPort,
                                    MessageBoxButtons.OK);
                }
                //the emulator will block until a client eg. HyperTerm connects
                Socket clientSocket = this.serverSocket.Accept();
                this.Stream = new NetworkStream(clientSocket, true);
            }
        }

        protected override void UninitializeProtected()
        {
            base.UninitializeProtected();
            if (this.Stream != null)
            {
                this.serverSocket.Close();
                this.serverSocket = null;
                this.Stream.Close();
                this.Stream = null;
            }
        }

        #region properties
        public IPAddress ClientIpAddress
        {
            get { return this.clientIpAddress; }
            set { this.clientIpAddress = value; }
        }

        public int IpPort
        {
            get { return this.ipPort; }
            set { this.ipPort = value; }
        }

        public bool ShowWaitMessage
        {
            get { return this.showWaitMessage; }
            set { this.showWaitMessage = value; }
        }
        #endregion
    }
}
