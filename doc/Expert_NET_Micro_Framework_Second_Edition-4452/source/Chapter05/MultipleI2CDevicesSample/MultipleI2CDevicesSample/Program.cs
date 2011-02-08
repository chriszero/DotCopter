using System;
using Microsoft.SPOT.Hardware;

namespace MultipleI2CDevicesSample
{
    public class Program
    {
        public static void Main()
        {
            I2CDevice2 device1 = new I2CDevice2(new I2CDevice.Configuration(117, 100));
            I2CDevice2 device2 = new I2CDevice2(new I2CDevice.Configuration(118, 100));
            device1.Dispose();
            device2.Dispose();
        }
    }
}
