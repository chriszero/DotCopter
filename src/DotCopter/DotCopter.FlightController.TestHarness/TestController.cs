using System;
using DotCopter.Commons.Logging;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController.TestHarness
{
    public class TestController
    {
        public TestController(IMotorMixer mixer, AxesController pid, IGyro gyro, IRadio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            long previousTime = DateTime.Now.Ticks;
            long lastRadioTime = 0;

            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    //Debug.Print((loopSettings.LoopUnit/(float) (currentTime - lastRadioTime)).ToString());
                    lastRadioTime = currentTime;
                    radio.Update();
                    gyro.Update();
                    pid.Update(radio.Axes, gyro.Axes, (float) (currentTime - lastRadioTime)/loopSettings.LoopUnit);
                    mixer.Update(radio.Throttle, pid.Axes);
                }
            }
        }
    }
}
