using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace SpiAdConverterSample
{
    /// <summary>
    /// Managed driver for a 4 channel 12-bit analog digital converter from National Semiconductor.
    /// </summary>
    public sealed class ADC124S101
    {
        public enum AdcChannel { ADC1, ADC2, ADC3, ADC4 };

        private readonly Cpu.Pin chipSelectPin;
        //the maximum input voltage depends on supply voltage,
        //so the supply voltage is needed to calculate the measured voltage
        private readonly float supplyVoltage;
        private readonly SPI spi;
        private readonly ushort[] writeBuffer = new ushort[1];
        private readonly ushort[] readBuffer = new ushort[1];

        public ADC124S101(Cpu.Pin chipSelectPin,
                          float supplyVoltage,
                          uint clockRateKHz,
                          SPI.SPI_module spiModule)
        {
            if (chipSelectPin == Cpu.Pin.GPIO_NONE)
                throw new ArgumentOutOfRangeException("chipSelectPin");
            if (supplyVoltage <= 0.0f)
                throw new ArgumentOutOfRangeException("supplyVoltage");
            this.chipSelectPin = chipSelectPin;
            this.supplyVoltage = supplyVoltage;

            SPI.Configuration config = new SPI.Configuration(
                                                 chipSelectPin, //chip select port
                                                 false,         //IC is accessed when chip select is low
                                                 1,             //setup time 1 ms, is actually min 10 ns
                                                 1,             //hold chip select 1 ms after transfer
                                                 true,          //clock line is high if device is not selected
                                                 false,         //data is sampled at falling edge of clock
                                                 clockRateKHz,  //possible 10000 - 20000 KHz
                                                 spiModule      //select SPI bus 
                                                 );
            this.spi = new SPI(config);
        }

        public float GetVoltage(AdcChannel channel)
        {
            this.writeBuffer[0] = (ushort)channel; //select ADC channel
            this.readBuffer[0] = 0; //reset buffer for safety
            spi.WriteRead(writeBuffer, readBuffer); //trigger conversion and read result
            //raw value is 12 Bit the 4 most significant bits of the ushort are zero
            ushort rawValue = readBuffer[0];
            return rawValue / 4096.0f * this.supplyVoltage; //smallest change is 1/4096 of supply voltage
        }

        public Cpu.Pin ChipSelectPin
        {
            get { return this.chipSelectPin; }
        }

        public float SupplyVoltage
        {
            get { return this.supplyVoltage; }
        }
    }
}
