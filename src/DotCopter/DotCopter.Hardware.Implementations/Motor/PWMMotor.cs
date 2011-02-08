using DotCopter.Commons.Utilities;
using DotCopter.Framework.PulseWidthModulation;
using DotCopter.Hardware.Motor;

namespace DotCopter.Hardware.Implementations.Motor
{
    public class PWMMotor : IMotor
    {
        private IPulseWidthModulation _pwmOutput;
        private MotorSettings _settings;
        private const uint _period = 2000000; //2ms or 500Hz

        public PWMMotor(IPulseWidthModulation pwmOutput, MotorSettings settings)
        {
            _pwmOutput = pwmOutput;
            _settings = settings;
            SetSafe();
        }
        public void Update(float throttle)
        {
            throttle = Logic.Constrain(throttle, _settings.MinimumOutput, _settings.MaximumOutput);
            throttle = _settings.MotorScale.Calculate(throttle);
            _pwmOutput.SetPulse(_period, (uint)throttle);
        }

        public void SetSafe()
        {
            float throttle = _settings.SafeOutput;
            throttle = _settings.MotorScale.Calculate(throttle);
            _pwmOutput.SetPulse(_period, (uint)throttle);
        }
    }
}
