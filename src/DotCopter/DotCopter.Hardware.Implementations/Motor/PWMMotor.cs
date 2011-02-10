using DotCopter.Commons.Utilities;
using DotCopter.Framework.PWM;
using DotCopter.Hardware.Motor;

namespace DotCopter.Hardware.Implementations.Motor
{
    public class PWMMotor : Hardware.Motor.Motor
    {
        private readonly PWM _pwmOutput;
        private readonly MotorSettings _settings;

        public PWMMotor(PWM pwmOutput, MotorSettings settings)
        {
            _pwmOutput = pwmOutput;
            _settings = settings;
            SetSafe();
        }
        public override void Update(float throttle)
        {
            throttle = Logic.Constrain(throttle, _settings.MinimumOutput, _settings.MaximumOutput);
            throttle = _settings.MotorScale.Calculate(throttle);
            _pwmOutput.SetPulse((uint)throttle);
        }

        public override sealed void SetSafe()
        {
            float throttle = _settings.SafeOutput;
            throttle = _settings.MotorScale.Calculate(throttle);
            _pwmOutput.SetPulse((uint)throttle);
        }
    }
}
