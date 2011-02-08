using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.SPOT.Emulator;
using Microsoft.SPOT.Emulator.Com;

namespace SerialPortSampleEmulator
{
    public partial class Form1 : Form
    {
        private readonly Emulator emulator;

        public Form1(Emulator emulator)
        {
            if (emulator == null)
                throw new ArgumentNullException("emulator");

            InitializeComponent();

            this.ActiveControl = this.com1ToAppTextBox;

            this.emulator = emulator;

            Thread comPortWatcherThread = new Thread(new ThreadStart(ComPortWatchThreadMethod));
            comPortWatcherThread.IsBackground = true;
            comPortWatcherThread.Start();
        }

        private void ComPortWatchThreadMethod()
        {
            ComPortToMemoryStream comPort1 = this.emulator.FindComponentById("COM1") as ComPortToMemoryStream;
            byte[] buffer = new byte[1024];
            while (true)
            {
                int com1Count = 0;
                comPort1.Invoke(new MethodInvoker(
                        delegate
                        {
                            com1Count = comPort1.StreamOut.Read(buffer, 0, buffer.Length);
                        }));
               if (com1Count > 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, com1Count);
                    Invoke(new MethodInvoker(
                        delegate
                        {
                            this.appToCom1TextBox.AppendText(text);
                        }));
                }
                Thread.Sleep(25);
            }
        }

        private void sendToAppButton_Click(object sender, EventArgs e)
        {
            ComPortToMemoryStream comPort1 = this.emulator.FindComponentById("COM1") as ComPortToMemoryStream;
            byte[] bytes = Encoding.UTF8.GetBytes(this.com1ToAppTextBox.Text);
            comPort1.BeginInvoke(new MethodInvoker(
                delegate
                {
                    comPort1.StreamOut.Write(bytes, 0, bytes.Length);
                }));
        }
    }
}