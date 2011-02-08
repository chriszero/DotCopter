using System;
using Microsoft.SPOT.Emulator.Spi;

namespace Kuehner.SPOT.Emulator
{
    public sealed class ADC124S101 : SpiDevice
    {
        public enum AdcChannel { ADC1, ADC2, ADC3, ADC4 };

        private uint supplyVoltage = 5000;
        private readonly float[] voltages = new float[4];

        protected override ushort[] Write(ushort[] data)
        {
            //what was written to the bus is in data
            int channel = data[0]; //selected ADC channel
            float supplyVoltageVolt = this.supplyVoltage / 1000.0f;
            ushort rawValue = (ushort)(this.voltages[channel] / supplyVoltageVolt * 4096.0f + 0.5f);
            return new ushort[] { rawValue }; //return what will be read
        }

        /// <summary>The supply voltage for the IC in Milli-Volts./// </summary>
        public uint SupplyVoltage
        {
            get { return this.supplyVoltage; }
            set { this.supplyVoltage = value; }
        }

        /// <summary>Returns the measured voltage in Volt of the specified ADC channel.</summary>
        public float this[AdcChannel channel]
        {
            get { return this.voltages[(int)channel]; }
            set
            {
                if (value < 0 || value >= this.supplyVoltage / 1000.0f)
                    throw new ArgumentOutOfRangeException("value", value, null);
                this.voltages[(int)channel] = value;
            }
        }
    }
}
