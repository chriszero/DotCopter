using System;
using System.Threading;
using Microsoft.SPOT;

namespace I2CTemperatureSensorSample
{
    public class Program
    {
        private const byte sensorAddress = 72;

        public static void Main()
        {
            TMP100Sensor temperatureSensor = new TMP100Sensor(sensorAddress);
            while (true)
            {
                float temperature = temperatureSensor.Temperature();
                Debug.Print("Temperature: " + temperature.ToString("F4") + " ° Celsius");
                Thread.Sleep(100);
            }
        }
    }
}
