using DotCopter.Avionics;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Implementations.Bus;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Gyro
{
    public class ITG3200 : Hardware.Gyro.Gyro
    {
        private readonly float _factor;
        private byte[] _buffer = new byte[6];
        private float _zeroPitch;
        private float _zeroRoll;
        private float _zeroYaw;
        private I2CBus _twiBus;
        private I2CDevice.Configuration _i2cConfiguration = new I2CDevice.Configuration(0x69,400);

        public ITG3200(I2CBus twiBus, float factor) : base(new AircraftPrincipalAxes())
        {
            _twiBus = twiBus;
            _factor = factor;
            Initialize();
        }

        private void Initialize()
        {
            //Initialize the gyro
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x3E, 0x80 }, 100); // send a reset to the device
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x15, 0x00 }, 100); // 1kHz sample rate
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x16, 0x1D }, 100); // 10Hz low pass filter
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x17, 0x05 }, 100); // enable send raw values
            _twiBus.Write(_i2cConfiguration, new byte[] { 0x3E, 0x01 }, 100); // use internal oscillator
            Zero();
        }

        public override void Update()
        {
            _twiBus.ReadRegister(_i2cConfiguration, 0x1D, _buffer, 100);
            float pitch = (short) (_buffer[0] << 8) | _buffer[1];
            float roll = (short) (_buffer[2] << 8) | _buffer[3];
            float yaw = (short) (_buffer[4] << 8) | _buffer[5];

            Axes.Pitch = pitch/_factor - _zeroPitch;
            Axes.Roll = roll/_factor - _zeroRoll;
            Axes.Yaw = yaw/_factor - _zeroYaw;
        }

        public override void Zero()
        {
            //Average the gyro for 50 readings to create zero
            float[] pitchData = new float[50];
            float[] rollData = new float[50];
            float[] yawData = new float[50];
            for (int i = 0; i < 50; i++)
            {
                Update();
                pitchData[i] = Axes.Pitch;
                rollData[i] = Axes.Roll;
                yawData[i] = Axes.Yaw;
                //Thread.Sleep(10);
            }

            Sort(ref pitchData);
            Sort(ref rollData);
            Sort(ref yawData);

            _zeroPitch = pitchData[25];
            _zeroRoll = rollData[25];
            _zeroYaw = yawData[25];
        }

        //Classic Bubble Sort
        private static void Sort(ref float[] data)
        {
            for (var i = 0; i < data.Length - 1; i++)
            {
                for (var j = data.Length-1; j > i; j--)
                {
                    if (data[j] >= data[j - 1]) continue;
                    var temp = data[j];
                    data[j] = data[j - 1];
                    data[j - 1] = temp;
                }
            }
        }

    }
}
