using DotCopter.Framework.PulseWidthModulation;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace DotCopter.Framework.Implementations.GHIElectronics.PulseWidthModulation
{
    public class PulseWidthModulation : IPulseWidthModulation
    {
        private readonly PWM _pwm;
        private uint _period;

        public PulseWidthModulation(Cpu.Pin pin)
        {
            _pwm = new PWM((PWM.Pin) pin);
        }

        public void SetPulse(uint highTime)
        {
            _pwm.SetPulse(_period, highTime);
        }

        public void SetPulse(uint period,uint highTime)
        {
            _period = period;
            SetPulse(highTime);
        }
    }
}
