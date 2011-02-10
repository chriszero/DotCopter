using System;

namespace DotCopter.Framework.PWM
{
    public abstract class PWM
    {
        protected uint _period;
        public abstract void SetPulse(uint highTime);

        protected PWM(uint period)
        {
            _period = period;
        }
    }
}