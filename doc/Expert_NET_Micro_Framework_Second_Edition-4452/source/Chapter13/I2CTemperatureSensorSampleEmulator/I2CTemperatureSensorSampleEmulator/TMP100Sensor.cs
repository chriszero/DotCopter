using System;
using System.Threading;
using Microsoft.SPOT.Emulator.I2c;
using Microsoft.SPOT.Emulator.Time;

namespace Kuehner.SPOT.Emulator
{
    public class TMP100Sensor : I2cDevice
    {
        private float temperature = 20.0f;
        private ushort temperatureRegister;
        private bool readTemperature;

        private const byte REGISTER_Control = 0x01;
        private const byte REGISTER_Temperature = 0x00;

        private const byte CONTROL_EnergyModeShutdown = 0x01;
        private const byte CONTROL_DataLengthTwelveBits = 0x60;
        private const byte CONTROL_OneShot = 0x80;

        protected override void DeviceWrite(byte[] data)
        {
            switch (data[0])
            {
                case REGISTER_Control:
                    if ((data[1] & CONTROL_OneShot) != 0) //make conversion
                    {
                        //only 12-bit conversion implemented
                        if ((data[1] & CONTROL_DataLengthTwelveBits) == CONTROL_DataLengthTwelveBits)
                        {
                            //conversion takes 600 ms for 12 Bit in the worst case
                            //temperature is available after this delay
                            new TimingServices.Timer(this.Emulator, 
                                                     new TimerCallback(Convert), 
                                                     null, 
                                                     600, //ms
                                                     -1); //one shot 
                         }
                    }
                    break;
                case REGISTER_Temperature: //read result of conversion from register
                    this.readTemperature = true;
                    break;
            }
        }

        private void Convert(object state)
        {
            this.temperatureRegister = (ushort)(this.temperature * 256.0);
            this.temperatureRegister &= 0xFFF0; //keep only first 12 bit
        }

        protected override void DeviceRead(byte[] data)
        {
            if(this.readTemperature) //if read command was recevied
            {
                data[0] = (byte)(this.temperatureRegister >> 8);
                data[1] = (byte)this.temperatureRegister;
                this.readTemperature = false;
            }
        }

        public float Temperature
        {
            get { return this.temperature; }
            set 
            {
                if (value < -128.0f || value >= 128.0f)
                    throw new ArgumentOutOfRangeException("value", value, null);
                this.temperature = value;
            }
        }
    }
}
