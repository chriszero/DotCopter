using DotCopter.Avionics;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Motor;

namespace DotCopter.ControlAlgorithms.Implementations.Mixing
{
    public class QuadMixer : MotorMixer
    {
        private readonly Motor _frontMotor;
        private readonly Motor _rearMotor;
        private readonly Motor _leftMotor;
        private readonly Motor _rightMotor;

        public QuadMixer(Motor frontMotor, Motor rearMotor, Motor leftMotor, Motor rightMotor)
        {
            _frontMotor = frontMotor;
            _rearMotor = rearMotor;
            _leftMotor = leftMotor;
            _rightMotor = rightMotor;
            SetSafe();
        }

        public override sealed void SetSafe()
        {
            _frontMotor.SetSafe();
            _rearMotor.SetSafe();
            _leftMotor.SetSafe();
            _rightMotor.SetSafe();
        }

        public override void Update(float throttle, AircraftPrincipalAxes output)
        {
            _frontMotor.Update(throttle + output.Pitch - output.Yaw);
            _rearMotor.Update(throttle - output.Pitch - output.Yaw);
            _leftMotor.Update(throttle + output.Roll + output.Yaw);
            _rightMotor.Update(throttle - output.Roll + output.Yaw);
        }
    }
}
