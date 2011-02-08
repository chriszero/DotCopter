using System;
using System.Windows.Forms;
using Microsoft.SPOT.Emulator;
using Microsoft.SPOT.Emulator.Gpio;

namespace GpioPortSampleEmulator
{
    public partial class Form1 : Form
    {
        private readonly GpioPort gpioIn0;
        private readonly GpioPort gpioIn1;

        delegate void PortWriteDelegate(bool state);

        public Form1(Emulator emulator)
        {
            if (emulator == null)
                throw new ArgumentNullException("emulator");
            InitializeComponent();

            GpioPort gpioOut0 = (GpioPort)emulator.FindComponentById("GPIOOut0");
            this.gpioOut0CheckBox.Text = "GPIO Output Port at Pin " + (int)gpioOut0.Pin;
            gpioOut0.OnGpioActivity += new GpioActivity(gpioOut0_OnGpioActivity);
            
            GpioPort gpioOut1 = (GpioPort)emulator.FindComponentById("GPIOOut1");
            this.gpioOut1CheckBox.Text = "GPIO Output Port at Pin " + (int)gpioOut1.Pin;
            gpioOut1.OnGpioActivity += new GpioActivity(gpioOut1_OnGpioActivity);

            this.gpioIn0 = (GpioPort)emulator.FindComponentById("GPIOIn0");
            this.gpioIn0CheckBox.Text = "GPIO Input Port at Pin " + (int)this.gpioIn0.Pin;

            this.gpioIn1 = (GpioPort)emulator.FindComponentById("GPIOIn1");
            this.gpioIn1Button.Text = "GPIO Input Port at Pin " + (int)this.gpioIn1.Pin;
        }

        private void gpioOut0_OnGpioActivity(GpioPort sender, bool edge)
        {
            BeginInvoke(new MethodInvoker(delegate { this.gpioOut0CheckBox.Checked = edge; }));
        }

        private void gpioOut1_OnGpioActivity(GpioPort sender, bool edge)
        {
            BeginInvoke(new MethodInvoker(delegate { this.gpioOut0CheckBox.Checked = edge; }));
        }

        private void gpioIn0CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.gpioIn0.BeginInvoke(new MethodInvoker(delegate 
                { this.gpioIn0.Write(this.gpioIn0CheckBox.Checked); }));
        }

        private void gpioIn1Button_MouseDown(object sender, MouseEventArgs e)
        {
            this.gpioIn1.BeginInvoke(new MethodInvoker(delegate { this.gpioIn1.Write(true); }));
        }

        private void gpioIn1Button_MouseUp(object sender, MouseEventArgs e)
        {
            this.gpioIn1.BeginInvoke(new MethodInvoker(delegate {this.gpioIn1.Write(false); }));
        }
    }
}