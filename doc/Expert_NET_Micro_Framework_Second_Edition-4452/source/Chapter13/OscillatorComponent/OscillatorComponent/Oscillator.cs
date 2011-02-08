using System;
using System.Threading;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Emulator;
using Microsoft.SPOT.Emulator.Time;
using Microsoft.SPOT.Emulator.Gpio;

namespace Kuehner.SPOT.Emulator
{
    public sealed class Oscillator : GpioPort
    {
        private int period = 500;
        private TimingServices.Timer timer;

        public Oscillator()
        {
            this.ModesAllowed = GpioPortMode.InputPort;
            this.ModesExpected = GpioPortMode.InputPort;
        }

        public override void InitializeComponent()
        {
            base.InitializeComponent();
            this.timer = new TimingServices.Timer(this.Emulator,
                                                  new TimerCallback(OnTimer),
                                                  null,
                                                  0,
                                                  this.period); 
        }

        public override void UninitializeComponent()
        {
            this.timer.AbortCompletion();
            base.UninitializeComponent();
        }

        private void OnTimer(object target)
        {
            bool status = Read();
            Write(!status);
        }

        /// <summary>
        /// The time interval between toggling the GPIO pin in milliseconds.
        /// </summary>
        public int Period
        {
            get { return this.period; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", "Period must be a positive number of ms.");
                this.period = value;
                if (this.timer != null)
                    this.timer.Change(0, value);
            }
        }
    }
}
