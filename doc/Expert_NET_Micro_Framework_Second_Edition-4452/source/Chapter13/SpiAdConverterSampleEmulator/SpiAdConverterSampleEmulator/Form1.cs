using System;
using System.Windows.Forms;
using Kuehner.SPOT.Emulator;

namespace SpiAdConverterSampleEmulator
{
    public partial class Form1 : Form
    {
        private readonly ADC124S101 adc;

        public Form1(ADC124S101 adc)
        {
            if (adc == null)
                throw new ArgumentNullException("adc");
            this.adc = adc;

            InitializeComponent();

            float supplyVoltageVolt = adc.SupplyVoltage / 1000.0f;
            this.maxVoltageLabel.Text = supplyVoltageVolt.ToString("F3") + " Volt";
            this.voltageTrackBar.Maximum = (int)(adc.SupplyVoltage - 1);
            this.voltageTrackBar.Value = (int)(this.voltageTrackBar.Maximum / 2.0 + 0.5);
            voltageTrackBar_Scroll(null, null);
        }

        private void voltageTrackBar_Scroll(object sender, EventArgs e)
        {
            float voltage = this.voltageTrackBar.Value / 1000.0f;
            this.adc.BeginInvoke(new MethodInvoker(delegate { this.adc[ADC124S101.AdcChannel.ADC1] = voltage; }));
            this.voltageLabel.Text = voltage.ToString("F3") + " Volt";
        }
    }
}