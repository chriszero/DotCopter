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
        public Controller(IMotorMixer mixer, AxesController pid, IGyro gyro, IRadio radio, ControllerLoopSettings loopSettings, MotorSettings motorSettings, ILogger logger)
        {
            TelemetryData telemetryData = new TelemetryData();
            long startupTime = DateTime.Now.Ticks;
            long previousTime = DateTime.Now.Ticks;
            long maxLoopTime = 0;
            long lastRadioTime = 0;
            long lastSensorTime = 0;
            long lastControlTime = 0;
            long lastMotorTime = 0;
            long lastTelemetryTime = 0;

            bool systemArmed = false;
            //bool loggingEnabled = false;
            //bool logFlushed = false;
            

            while (true)
            {
                long currentTime = DateTime.Now.Ticks - startupTime;
                long deltaTime = currentTime - previousTime;
                float deltaTimeFactor = (float)deltaTime / loopSettings.LoopUnit;
                previousTime = currentTime;

                if (deltaTime > maxLoopTime)
                {
                    maxLoopTime = deltaTime;
                }

                if (currentTime >= (lastRadioTime + loopSettings.RadioLoopPeriod))
                {
                    Debug.Print((loopSettings.LoopUnit/(currentTime-lastRadioTime)).ToString());
                    lastRadioTime = currentTime;
                    radio.Update();
                    systemArmed = radio.Throttle > motorSettings.MinimumOutput;
                    if (!systemArmed)
                    {
                        //Commented this out, we should send in a null logger into constructor if we dont want logging,
                        //and if we do want logging if the system is not armed we arent flying so this flush call requires 
                        //if statement which i assume was put in because of performance hit, we are
                        // are merely trying to ensire that the byte stream is written at shutdown
                        //loggingEnabled = radio.Gear;
                        //if (!loggingEnabled && !logFlushed)
                        //{
                            logger.Flush();
                            //logFlushed = true;
                        //}
                    }
                }

                if (systemArmed && (currentTime >= (lastSensorTime + loopSettings.SensorLoopPeriod)))
                {
                    Debug.Print((loopSettings.LoopUnit / (currentTime - lastSensorTime)).ToString());
                    lastSensorTime = currentTime;
                    gyro.Update();
                }

                if (systemArmed && (currentTime >= (lastControlTime + loopSettings.ControlAlgorithmPeriod)))
                {
                    Debug.Print((loopSettings.LoopUnit / (currentTime - lastControlTime)).ToString());
                    lastControlTime = currentTime;
                    pid.Update(radio.Axes,gyro.Axes,deltaTimeFactor);
                }

                if (currentTime >= (lastMotorTime + loopSettings.MotorLoopPeriod))
                {
                    Debug.Print((loopSettings.LoopUnit / (currentTime - lastMotorTime)).ToString());
                    if (systemArmed)
                        mixer.Update(radio.Throttle, pid.Axes);
                    else
                        mixer.SetSafe();
                    
                    lastMotorTime = currentTime;
                }
                
                if (systemArmed && (currentTime >= (lastTelemetryTime + loopSettings.TelemetryLoopPeriod)))
                {
                    telemetryData.Update(gyro.Axes, radio.Axes, pid.Axes, currentTime);
                    lastTelemetryTime = currentTime;
                    logger.Write(telemetryData);
                    Debug.GC(true);
                }
            }
        }
    }
}

