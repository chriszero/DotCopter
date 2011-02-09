using DotCopter.Avionics;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Bus;

namespace DotCopter.Hardware.Implementations.Gyro
{
    public class ITG3200 : TWISlave, IGyro
    {
        private readonly float _factor;
        private byte[] _buffer = new byte[6];
        private float _zeroPitch;
        private float _zeroRoll;
        private float _zeroYaw;

        public ITG3200(TWIBus twiBus, float factor): base(0x69, 400, twiBus) 
        {
            _factor = factor;
            Axes = new AircraftPrincipalAxes { Pitch = 0, Roll = 0, Yaw = 0 };
            Initialize();
        }

        private void Initialize()
        {
            //Initialize the gyro
            Write(new byte[] { 0x3E, 0x80 }, 100); // send a reset to the device
            Write(new byte[] { 0x15, 0x00 }, 100); // 1kHz sample rate
            Write(new byte[] { 0x16, 0x1D }, 100); // 10Hz low pass filter
            Write(new byte[] { 0x17, 0x05 }, 100); // enable send raw values
            Write(new byte[] { 0x3E, 0x01 }, 100); // use internal oscillator
            Zero();
        }

        public void Update()
        {
            Write(new byte[] { 0x1D }, 100);
            Read(ref _buffer, 100);

            //cast to short is necessary due to the gyro registers being 2's compliment shorts, leaving them as int will cause corruption
            float pitch = (short)(_buffer[0] << 8 | _buffer[1]) / _factor;
            float roll = (short)(_buffer[2] << 8 | _buffer[3]) / _factor;
            float yaw = (short)(_buffer[4] << 8 | _buffer[5]) / _factor;

            Axes.Pitch = pitch - _zeroPitch;
            Axes.Roll = roll - _zeroRoll;
            Axes.Yaw = yaw - _zeroYaw;
        }

        private void Zero()
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

        public AircraftPrincipalAxes Axes { get; set; }
    }
}
