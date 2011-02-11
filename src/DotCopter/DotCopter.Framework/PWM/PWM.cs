using System;

namespace DotCopter.Framework.PWM
{
    public abstract class PWM
    {
        protected uint _period;
        public abstract void SetPulse(uint highTime);

        protected PWM(Periods period) {
            _period = (uint)period;
        }

        protected PWM(uint period)
        {
            _period = period;
        }

        public enum Periods : uint {
            /// <summary>
            /// Standard rate
            /// </summary>
            P50Hz = 20 * 1000000,
            /// <summary>
            /// Turbo PWM
            /// </summary>
            P400Hz = 25 * 100000,
            P500Hz = 2 * 1000000,
        }
    }
}