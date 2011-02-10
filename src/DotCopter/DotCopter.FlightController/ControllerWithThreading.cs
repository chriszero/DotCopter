using System;
using DotCopter.Commons.Logging;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController
{
    public class ControllerWithThreading
    {
        //private readonly Motors _motors;
        private readonly AxesController _pid;
        private readonly Gyro _gyro;
        private readonly Radio _radio;
        private readonly ControllerLoopSettings _loopSettings;
        private readonly ILogger _logger;

        private long _previousTime = DateTime.Now.Ticks;
        private long _maxLoopTime;
        private long _lastRadioTime;
        private long _lastSensorTime;
        private long _lastControlTime;
        private long _lastMotorTime;
        private long _lastTelemetryTime;

        private bool _running;


        //public ControllerWithThreading(IMotors motors, AxesController pid, IGyro gyro, Radio radio, ControllerLoopSettings loopSettings, ILogger logger)
        //{
        //    _motors = motors;
        //    _pid = pid;
        //    _gyro = gyro;
        //    _radio = radio;
        //    _loopSettings = loopSettings;
        //    _logger = logger;
        //}

        public void Start()
        {
            _running = true;
            ControlLoop();
        }

        public void Stop()
        {
            _running = false;
        }

        private void ControlLoop()
        {
            //_logger.Write("TimeStamp,Throttle,pitchSP,pitchPV,pitchOP");

            while (_running)
            {
                long currentTime = DateTime.Now.Ticks;
                long deltaTime = currentTime - _previousTime;
                float deltaTimeFactor = (float)deltaTime / _loopSettings.LoopUnit;
                _previousTime = currentTime;

                if (deltaTime > _maxLoopTime)
                {
                    _maxLoopTime = deltaTime;
                }

                if (currentTime > (_lastRadioTime + _loopSettings.RadioLoopPeriod))
                {
                    _radio.Update();
                    _lastRadioTime = currentTime;
                }

                if (currentTime > (_lastSensorTime + _loopSettings.SensorLoopPeriod))
                {
                    _gyro.Update();
                    _lastSensorTime = currentTime;
                }

                if (currentTime > (_lastControlTime + _loopSettings.ControlAlgorithmPeriod))
                {
                    _pid.Update(_radio.Axes, _gyro.Axes, deltaTimeFactor);
                    _lastControlTime = currentTime;
                }

                if (currentTime > (_lastMotorTime + _loopSettings.MotorLoopPeriod))
                {
                    //_motors.Update(_radio.Throttle , _pid.Axes);
                    _lastMotorTime = currentTime;
                }

                if (currentTime > (_lastTelemetryTime + _loopSettings.TelemetryLoopPeriod))
                {
                    _lastTelemetryTime = currentTime;

                    //_logger.Write(
                    //    currentTime + "," +
                    //    _radio.Throttle + "," +
                    //    _radio.Axes.Pitch + "," +
                    //    _gyro.Axes.Pitch + "," +
                    //    _pid.Axes.Pitch);
                }
            }

            Debug.Print("exiting control loop");
        }
    }
}

