using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace I2CSample
{
    public class Program
    {
        public static void Main()
        {
            I2CDevice.Configuration config = new I2CDevice.Configuration(58, //address
                                                                         100 //clockrate in KHz
                                                                         );
            I2CDevice device = new I2CDevice(config);

            //prepare buffer to write byte AA
            byte[] outBuffer = new byte[] { 0xAA };
            I2CDevice.I2CWriteTransaction writeTransaction = device.CreateWriteTransaction(outBuffer);

            //prepare buffer to read four bytes
            byte[] inBuffer = new byte[4];
            I2CDevice.I2CReadTransaction readTransaction = device.CreateReadTransaction(inBuffer);

            //execute both transactions
            I2CDevice.I2CTransaction[] transactions =
                new I2CDevice.I2CTransaction[] { writeTransaction, readTransaction };
            int transferred = device.Execute(transactions,
                                             100 //timeout in ms
                                             );
            //transferred bytes should be 1 + 4 = 5
        }
    }
}
