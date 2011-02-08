using System;
using System.IO.Ports;
using Microsoft.SPOT.Emulator.Com;

namespace Kuehner.SPOT.Emulator
{
    public class ComPortToPhysicalPcSerialPort : ComPortToStream
    {
        private SerialPort serialPort;
        private string physicalPortName = "COM1";
        private int baudrate = 9600;
        private int readTimeout = 1000;
        private Handshake handshake = Handshake.None;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();
            if (this.Stream == null)
            {
                this.serialPort = new SerialPort(this.physicalPortName, this.baudrate);
                this.serialPort.ReadTimeout = this.readTimeout;
                this.serialPort.Handshake = this.handshake;
                this.serialPort.Open();
                this.Stream = this.serialPort.BaseStream;
            }
        }

        protected override void UninitializeProtected()
        {
            base.UninitializeProtected();
            if (this.Stream != null)
            {
                this.serialPort.Close(); //also closes the underlying stream
                this.serialPort = null;
                this.Stream = null;
            }
        }

        #region properties
        public string PhysicalPortName
        {
            get { return this.physicalPortName; }
            set { this.physicalPortName = value; }
        }

        public int Baudrate
        {
            get { return this.baudrate; }
            set { this.baudrate = value; }
        }

        public int ReadTimeout
        {
            get { return this.readTimeout; }
            set { this.readTimeout = value; }
        }

        public Handshake Handshake
        {
            get { return this.handshake; }
            set { this.handshake = value; }
        }
        #endregion
    }
}
