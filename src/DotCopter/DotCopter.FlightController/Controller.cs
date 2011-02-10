using System;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController
{
    public class Controller
    {
        public Controller(MotorMixer mixer, AxesController pid, Gyro gyro, Radio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            TelemetryData telemetryData = new TelemetryData();
            long previousTime = DateTime.Now.Ticks;
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;
            long lastTelemetryTime = 0;

            bool systemArmed = false;
            

            while (true)
            {
                long currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    //Debug.Print((loopSettings.LoopUnit/(float)(currentTime-lastRadioTime)).ToString());
                    lastRadioTime = currentTime;
                    radio.Update();
                    systemArmed = radio.Throttle > motorSettings.MinimumOutput;
                    if (!systemArmed)
                        logger.Flush();
                }

               
                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastSensorTime + loopSettings.SensorLoopPeriod)))
                {
                    //Debug.Print((loopSettings.LoopUnit / (float)(currentTime - lastSensorTime)).ToString());
                    lastSensorTime = currentTime;
                    gyro.Update();
                }

                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod)))
                {
                    //Debug.Print((loopSettings.LoopUnit / (float)(currentTime - lastControlTime)).ToString());
                    lastControlTime = currentTime;
                    pid.Update(radio.Axes, gyro.Axes, (float) (currentTime - previousTime)/loopSettings.LoopUnit);
                    previousTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (currentTime >= (lastMotorTime + loopSettings.MotorLoopPeriod))
                {
                    //Debug.Print((loopSettings.LoopUnit / (float)(currentTime - lastMotorTime)).ToString());
                    if (systemArmed)
                        mixer.Update(radio.Throttle, pid.Axes);
                    else
                        mixer.SetSafe();
                    
                    lastMotorTime = currentTime;
                }

                currentTime = DateTime.Now.Ticks;
                if (systemArmed && (currentTime >= (lastTelemetryTime + loopSettings.TelemetryLoopPeriod)))
                {
                    //Debug.Print((loopSettings.LoopUnit / (float)(currentTime - lastTelemetryTime)).ToString());
                    telemetryData.Update(gyro.Axes, radio.Axes, pid.Axes, currentTime);
                    lastTelemetryTime = currentTime;
                    logger.Write(telemetryData);
                }
            }
        }
    }
}

