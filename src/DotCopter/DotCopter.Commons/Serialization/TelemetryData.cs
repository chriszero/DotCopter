using System;
using DotCopter.Avionics;
using DotCopter.Commons.Utilities;

namespace DotCopter.Commons.Serialization
{
    public class TelemetryData : INETMFSerializable
    {
        private  AircraftPrincipalAxes  _gyroAxes;
        private  AircraftPrincipalAxes _radioAxes;
        private  AircraftPrincipalAxes _pidAxes;
        private long _currentTime;
        private byte[] _buffer;
        public TelemetryData()
        {
            _gyroAxes = new AircraftPrincipalAxes {Pitch = 0, Roll = 0, Yaw = 0};
            _radioAxes = new AircraftPrincipalAxes { Pitch = 0, Roll = 0, Yaw = 0 };
            _pidAxes = new AircraftPrincipalAxes { Pitch = 0, Roll = 0, Yaw = 0 };
            _currentTime = DateTime.Now.Ticks;
            _buffer = new byte[44];
        }

        public void Update(AircraftPrincipalAxes gyroAxes, AircraftPrincipalAxes radioAxes, AircraftPrincipalAxes pidAxes, long currentTime)
        {
            _gyroAxes = gyroAxes;
            _radioAxes = radioAxes;
            _pidAxes = gyroAxes;
            _currentTime = currentTime;
        }

        public byte[] Serialize()
        {
            BitConverter.ToBytes(ref _buffer, 0, _currentTime);
            BitConverter.ToBytes(ref _buffer, 8, _gyroAxes.Pitch);
            BitConverter.ToBytes(ref _buffer, 12, _gyroAxes.Roll);
            BitConverter.ToBytes(ref _buffer, 16, _gyroAxes.Yaw);

            BitConverter.ToBytes(ref _buffer, 20, _radioAxes.Pitch);
            BitConverter.ToBytes(ref _buffer, 24, _radioAxes.Roll);
            BitConverter.ToBytes(ref _buffer, 28, _radioAxes.Yaw);

            BitConverter.ToBytes(ref _buffer, 32, _pidAxes.Pitch);
            BitConverter.ToBytes(ref _buffer, 36, _pidAxes.Roll);
            BitConverter.ToBytes(ref _buffer, 40, _pidAxes.Yaw);

            return _buffer;
        }

        public object Deserialize(byte[] buffer)
        {
            _currentTime = BitConverter.ToLong(buffer, 0);
            _gyroAxes.Pitch = BitConverter.ToFloat(buffer, 8);
            _gyroAxes.Roll = BitConverter.ToFloat(buffer, 12);
            _gyroAxes.Yaw = BitConverter.ToFloat(buffer, 16);

            _radioAxes.Pitch = BitConverter.ToFloat(buffer, 20);
            _radioAxes.Roll = BitConverter.ToFloat(buffer, 24);
            _radioAxes.Yaw = BitConverter.ToFloat(buffer, 28);

            _pidAxes.Pitch = BitConverter.ToFloat(buffer, 32);
            _pidAxes.Roll = BitConverter.ToFloat(buffer, 36);
            _pidAxes.Yaw = BitConverter.ToFloat(buffer, 40);
            return this;
        }

        public override string ToString()
        {
            //csv format
            return _currentTime + "," +
                   _gyroAxes.Pitch + "," +
                   _gyroAxes.Roll + "," +
                   _gyroAxes.Yaw + "," +
                   _radioAxes.Pitch + "," +
                   _radioAxes.Roll + "," +
                   _radioAxes.Yaw + "," +
                   _pidAxes.Pitch + "," +
                   _pidAxes.Roll + "," +
                   _pidAxes.Yaw;
        }
    }
}
