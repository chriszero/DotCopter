using DotCopter.Avionics;
using DotCopter.Commons.Utilities;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Motor;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Hardware.Implementations.Motor
{
    public class DefaultMotors : TWISlave, IMotors
    {
        private readonly MotorSettings _settings;
        private readonly byte[] _buffer = new byte[8];

        private const float SafeCommand = 0;
        private const float MaxCommand = 100;

        private float _front;
        private float _left;
        private float _rear;
        private float _right;

        public DefaultMotors(TWIBus twiBus, MotorSettings settings)
            : base(0x42, 400, twiBus)
        {
            _settings = settings;
            SetSafe();
        }
        
        public void SetSafe()
        {
            byte[] buffer = new byte[8];
            Utility.InsertValueIntoArray(buffer, 0, 2, (uint)_settings.MotorScale.Calculate(SafeCommand));
            Utility.InsertValueIntoArray(buffer, 2, 2, (uint)_settings.MotorScale.Calculate(SafeCommand));
            Utility.InsertValueIntoArray(buffer, 4, 2, (uint)_settings.MotorScale.Calculate(SafeCommand));
            Utility.InsertValueIntoArray(buffer, 6, 2, (uint)_settings.MotorScale.Calculate(SafeCommand));
            Write(buffer, 100);
            
        }

        public void Update(float throttle, AircraftPrincipalAxes output)
        {
            _front = Logic.Constrain(throttle + output.Pitch - output.Yaw,_settings.MinimumOutput,_settings.MaximumOutput);
            _rear = Logic.Constrain(throttle - output.Pitch - output.Yaw, _settings.MinimumOutput, _settings.MaximumOutput);
            _left = Logic.Constrain(throttle + output.Roll + output.Yaw, _settings.MinimumOutput, _settings.MaximumOutput);
            _right = Logic.Constrain(throttle - output.Roll + output.Yaw, _settings.MinimumOutput, _settings.MaximumOutput);

            Utility.InsertValueIntoArray(_buffer, 0, 2, (uint)_settings.MotorScale.Calculate(_left));
            Utility.InsertValueIntoArray(_buffer, 2, 2, (uint)_settings.MotorScale.Calculate(_rear));
            Utility.InsertValueIntoArray(_buffer, 4, 2, (uint)_settings.MotorScale.Calculate(_right));
            Utility.InsertValueIntoArray(_buffer, 6, 2, (uint)_settings.MotorScale.Calculate(_front));

            Write(_buffer, 100);
        }

    }
}
