using DotCopter.Framework.PulseWidthModulation;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;

namespace DotCopter.Framework.Implementations.SecretLabs.PulseWidthModulation
{
    public class PulseWidthModulation : IPulseWidthModulation
    {
        private PWM _pwm;
        private uint _period;

        public PulseWidthModulation(Cpu.Pin pin, uint period)
        {
            _pwm = new PWM(pin);
            _period = period;
        }

        public void SetPulse(uint highTime)
        {
            _pwm.SetPulse(_period,highTime);
        }

        public void SetPulse(uint period, uint highTime)
        {
            _period = period;
            SetPulse(highTime);
        }
    }
}
