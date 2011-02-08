using System;
using System.Windows.Forms;
using Kuehner.SPOT.Emulator;

namespace I2CTemperatureSensorSampleEmulator
{
    public partial class Form1 : Form
    {
        private readonly TMP100Sensor temperatureSensor;

        public Form1(TMP100Sensor temperatureSensor)
        {
            if (temperatureSensor == null)
                throw new ArgumentNullException("temperatureSensor");
            this.temperatureSensor = temperatureSensor;

            InitializeComponent();

            this.temperatureTrackBar.Value = (int)(this.temperatureSensor.Temperature * 1000.0);
            temperatureTrackBar_Scroll(null, null);
        }

        private void temperatureTrackBar_Scroll(object sender, EventArgs e)
        {
            float temperature = this.temperatureTrackBar.Value / 1000.0f;
            this.temperatureSensor.BeginInvoke(new MethodInvoker(delegate
              { this.temperatureSensor.Temperature = temperature; }));
            this.temperatureLabel.Text = temperature.ToString("F3") + " ° Celsius";
        }
    }
}