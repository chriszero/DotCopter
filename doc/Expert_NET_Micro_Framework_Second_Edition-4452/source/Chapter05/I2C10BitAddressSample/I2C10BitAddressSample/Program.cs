using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace I2C10BitAddressSample
{
    public class Program
    {
        public static void Main()
        {
            //our 10 bit address
            const ushort address10Bit = 0x1001; //binary 1000000001 = 129
            const byte addressMask = 0x78; //reserved address mask 011110XX for 10 bit addressing	

            //first MSB part of address
            ushort address1 = addressMask | (address10Bit >> 8); //is 7A and contains the two MSB of the 10 bit address
            I2CDevice.Configuration config = new I2CDevice.Configuration(address1, 100);
            I2CDevice device = new I2CDevice(config);

            //second LSB part of address
            byte address2 = (byte)(address10Bit & 0xFF); //the other 8 bits (LSB)
            byte[] address2OutBuffer = new byte[] { address2 };
            I2CDevice.I2CWriteTransaction addressWriteTransaction = device.CreateWriteTransaction(address2OutBuffer);

            //prepare buffer to write data
            byte[] outBuffer = new byte[] { 0xAA };
            I2CDevice.I2CWriteTransaction writeTransaction = device.CreateWriteTransaction(outBuffer);

            //prepare buffer to read data
            byte[] inBuffer = new byte[4];
            I2CDevice.I2CReadTransaction readTransaction = device.CreateReadTransaction(inBuffer);

            //execute transactions
            I2CDevice.I2CTransaction[] transactions =
                new I2CDevice.I2CTransaction[] { addressWriteTransaction, writeTransaction, readTransaction };
            device.Execute(transactions, 100);
        }
    }
}
