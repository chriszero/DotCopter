using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.SPOT.Emulator.Com;

namespace Kuehner.SPOT.Emulator
{
    public class ComPortToClientSocket : ComPortToStream
    {
        private IPAddress hostIpAddress = IPAddress.Loopback;
        private int ipPort = 23;
        
        protected override void InitializeProtected()
        {
            base.InitializeProtected();
            if (this.Stream == null)
            {
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(this.hostIpAddress, this.ipPort);
                this.Stream = new NetworkStream(clientSocket, true);
            }
        }

        protected override void UninitializeProtected()
        {
            base.UninitializeProtected();
            if (this.Stream != null)
            {
                this.Stream.Close();
                this.Stream = null;
            }
        }

        #region properties
        public IPAddress HostIpAddress
        {
            get { return this.hostIpAddress; }
            set { this.hostIpAddress = value; }
        }

        public int IpPort
        {
            get { return this.ipPort; }
            set { this.ipPort = value; }
        }
        #endregion
    }
}
