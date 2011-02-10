using Microsoft.SPOT.Hardware;

namespace DotCopter.Framework.Implementations.GHIElectronics.PWM
{
    public class PWM : Framework.PWM.PWM
    {
        private readonly global::GHIElectronics.NETMF.Hardware.PWM _pwm;

        public PWM(Cpu.Pin pin, uint period) : base(period)
        {
            _pwm = new global::GHIElectronics.NETMF.Hardware.PWM((global::GHIElectronics.NETMF.Hardware.PWM.Pin) pin);
        }

        public override void SetPulse(uint highTime)
        {
            _pwm.SetPulse(_period, highTime);
        }

    }
}
