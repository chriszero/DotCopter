using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace I2CTemperatureSensorSample
{
    /// <summary>
    /// Managed driver for the TMP100 temperature sensor chip
    /// from Texas Instruments on the I2C bus.
    /// </summary>
    public class TMP100Sensor
    {
        #region constants
        private const byte clockRateKHz = 59;

        private const byte REGISTER_Control = 0x01; //command to configure sensor
        private const byte REGISTER_Temperature = 0x00; //command to request result

        private const byte CONTROL_EnergyModeShutdown = 0x01;
        private const byte CONTROL_DataLengthTwelveBits = 0x60;
        private const byte CONTROL_OneShot = 0x80;

        private const int conversionTime = 600; //ms The maximum time needed by the sensor to convert 12 bits

        private const int transactionTimeout = 1000; //ms
        #endregion

        private readonly byte address;
        private readonly I2CDevice device;

        public TMP100Sensor(byte address)
        {
            this.address = address;
            I2CDevice.Configuration config = new I2CDevice.Configuration(address, clockRateKHz);
            this.device = new I2CDevice(config);
        }

        public float Temperature()
        {
            //write to one shot bit in control register to trigger measurement, 
            //that means telling the sensor to capture the data.
            byte controlByte = CONTROL_OneShot | CONTROL_DataLengthTwelveBits | CONTROL_EnergyModeShutdown;
            byte[] captureData = new byte[] { REGISTER_Control, controlByte };
            WriteToDevice(captureData);

            //the conversion time is the maximum time it takes the sensor to convert a physical reading to bits
            Thread.Sleep(conversionTime);

            //prepare the control byte to tell the sensor to send the temperature register
            byte[] temperatureData = new byte[] { REGISTER_Temperature };
            WriteToDevice(temperatureData);

            //prepare the array of bytes that will hold the result comming back from the sensor
            byte[] inputData = new byte[2];
            ReadFromDevice(inputData);

            //get raw temperature register
            short rawTemperature = (short)((inputData[0] << 8) | inputData[1]);
            //convert raw temperature register to Celsius degrees
            //the highest 12 Bits of the 16 Bit-Signed-Integer (short) are used
            //one digit is 0.0625 ° Celsius, divide by 16 to shift right 4 Bits
            //this results in a division by 256
            float temperature = rawTemperature * (1 / 256.0f);
            return temperature;
        }

        private void WriteToDevice(byte[] outputData)
        {
            //create an I2C write transaction to be sent to the temperature sensor
            I2CDevice.I2CTransaction writeXAction = device.CreateWriteTransaction(outputData);

            //the I2C data is sent here to the temperature sensor
            int transferred = this.device.Execute(new I2CDevice.I2CTransaction[] { writeXAction },
                                                  transactionTimeout);

            //make sure the data was sent
            if (transferred != outputData.Length)
                throw new Exception("Could not write to device.");
        }

        private void ReadFromDevice(byte[] inputData)
        {
            //prepare a I2C read transation to be read from the temperature sensor
            I2CDevice.I2CTransaction readXAction = device.CreateReadTransaction(inputData);

            //the I2C data is received here from the temperature sensor
            int transferred = this.device.Execute(new I2CDevice.I2CTransaction[] { readXAction },
                                                  transactionTimeout);

            //make sure the data was received
            if (transferred != inputData.Length)
                throw new Exception("Could not read from device.");
        }

        public byte Address
        {
            get { return this.address; }
        }
    }
}
