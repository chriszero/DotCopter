using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace SpiAdConverterSample
{
    public class Program
    {
        public static void Main()
        {
            ADC124S101 adc = new ADC124S101(Cpu.Pin.GPIO_Pin10, //chip select port
                                            5,                  //supply voltage
                                            15000,              //clock rate in KHz
                                            SPI.SPI_module.SPI1 //first SPI bus
                                            );
            while (true)
            {
                float voltage = adc.GetVoltage(ADC124S101.AdcChannel.ADC1);
                Debug.Print("ADC1: " + voltage.ToString("F3") + " Volt");
                Thread.Sleep(10);  //give emulator time to react to Visual Studio
            }
        }
    }
}
