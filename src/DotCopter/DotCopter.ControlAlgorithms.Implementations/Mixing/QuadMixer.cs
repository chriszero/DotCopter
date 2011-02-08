using DotCopter.Avionics;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Motor;

namespace DotCopter.ControlAlgorithms.Implementations.Mixing
{
    public class QuadMixer : IMotorMixer
    {
        private readonly IMotor _frontMotor;
        private readonly IMotor _rearMotor;
        private readonly IMotor _leftMotor;
        private readonly IMotor _rightMotor;

        public QuadMixer(IMotor frontMotor, IMotor rearMotor, IMotor leftMotor, IMotor rightMotor)
        {
            _frontMotor = frontMotor;
            _rearMotor = rearMotor;
            _leftMotor = leftMotor;
            _rightMotor = rightMotor;
            SetSafe();
        }

        public void SetSafe()
        {
            _frontMotor.SetSafe();
            _rearMotor.SetSafe();
            _leftMotor.SetSafe();
            _rightMotor.SetSafe();
        }

        public void Update(float throttle, AircraftPrincipalAxes output)
        {
            _frontMotor.Update(throttle + output.Pitch - output.Yaw);
            _rearMotor.Update(throttle - output.Pitch - output.Yaw);
            _leftMotor.Update(throttle + output.Roll + output.Yaw);
            _rightMotor.Update(throttle - output.Roll + output.Yaw);
        }
    }
}
