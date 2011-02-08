using System;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using DotCopter.Hardware.Storage;
using Microsoft.SPOT;

namespace DotCopter.FlightController.Implementations.FezPanda
{

    public class Quadrocopter
    {
        private readonly ISDCard _sdCard;
        private readonly ControllerWithThreading _controller;
        private readonly ILogger _logger;
        private readonly TWIBus _twiBus;
        private readonly ProportionalIntegralDerivativeSettings[] _pidSettings;
        private readonly IRadio _radio;
        private readonly AxesController _axesController;
        private readonly IMotors _motors;
        private readonly IGyro _gyro;
        private bool _running;

        private DateTime _lastReceivedGearChange;
        private DateTime _firstReceivedGearChange;
        private int _gearChangeCount;
        private const int GearCountTimes=4;
        private const int GearCountSeconds = 3;
        
        public Quadrocopter()
        {
            //_sdCard = new SDCard();
            //if (Configuration.DebugInterface.GetCurrent() == Configuration.DebugInterface.Port.USB1)
            //    _sdCard.MountFileSystem();
            //else
            //    new TelemetryPresenter(_sdCard, (Cpu.Pin)FezPin.Digital.LED);
            //_firstReceivedGearChange = DateTime.MinValue;
            //_gearChangeCount = 0;
            //_logger = new PersistenceWriter(@"\SD\telemetry", new TelemetryFormatter());
            //_pidSettings = GetPIDSettings();
            //_twiBus = new TWIBus();
            //_motors = new DefaultMotors(_twiBus, GetMotorSettings());
            //_axesController = new AxesController(_pidSettings[0], _pidSettings[1], _pidSettings[2], true);
            //_gyro = new ITG3200(_twiBus, GetGyroFactor());
            //_radio = new DefaultRadio(_twiBus, GetRadioSettings());
            ////_radio.HaveChangedGearEvent += GearChanged;
            //ControllerLoopSettings loopSettings = GetLoopSettings();
            //_controller = new ControllerWithThreading(_motors, _axesController, _gyro, _radio, loopSettings, _logger);
            //while (true)
            //{
            //    if(!_running)
            //    {
            //        _radio.Update();
            //        Thread.Sleep(50);
            //    }
            //    Thread.Sleep(50);//not sure this might have to come out
            //}
        }

        private void StartUp()
        {
            Debug.Print("System Starting Up");
            if (!_sdCard.IsFileSystemMounted)
            {
                _sdCard.MountFileSystem();
            }
            _controller.Start();
            Debug.Print("Successfully Started Up");
        }

        private void ShutDown()
        {
            Debug.Print("System Shutting Down");
            _running = false;
            _controller.Stop();
            _sdCard.UnMountFileSystem();
            _motors.SetSafe();
            Debug.Print("Successfully Shut Down");
        }

        private void GearChanged(bool isOn)
        {
            Debug.Print("Got Gear Input");
            _lastReceivedGearChange = DateTime.Now;
            if (_firstReceivedGearChange == DateTime.MinValue)
            {
                _firstReceivedGearChange = _lastReceivedGearChange;
            }

            TimeSpan difference = _lastReceivedGearChange - _firstReceivedGearChange;
            if (difference.Seconds < GearCountSeconds)
            {
                _gearChangeCount++;
                
                if (!_running || _gearChangeCount == GearCountTimes)
                {
                    if (_running)
                    {
                        ShutDown();
                        ResetCount();
                    }
                    else
                    {
                        _running = true;
                        Thread thread = new Thread(StartUp);
                        thread.Start();
                        ResetCount();
                    }
                }
            }
            else
            {
                _gearChangeCount = 1;
                _firstReceivedGearChange = DateTime.Now;
            }
        }

        private void ResetCount()
        {
            _gearChangeCount = 0;
            _firstReceivedGearChange = DateTime.MinValue;
        }

        private static ProportionalIntegralDerivativeSettings[] GetPIDSettings()
        {
            ProportionalIntegralDerivativeSettings pitchSettings =
                new ProportionalIntegralDerivativeSettings(3.2F, 0F, -.5F, 2000F);

            ProportionalIntegralDerivativeSettings rollSettings =
                new ProportionalIntegralDerivativeSettings(3.2F, 0F, -.5F, 2000F);

            ProportionalIntegralDerivativeSettings yawSettings =
                new ProportionalIntegralDerivativeSettings(3F, 0F, 0F, 2000F);
            return new[] { pitchSettings, rollSettings, yawSettings };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(
                50,         //radioLoopFrequency
                50,        //sensorLoopFrequency
                50,        //controlAlgorithmFrequency
                50,        //motorLoopFrequency
                2,          //telemetryLoopFrequency
                10000000);  //loopUnit
        }

        private static MotorSettings GetMotorSettings()
        {
            return new MotorSettings(
                new Scale(0, 10, 1000), //motor scale
                0,                      //safeOutput
                15,                     //minimumOutput
                95);                    //maximimOutput
        }

        private static RadioSettings GetRadioSettings()
        {
            return new RadioSettings(
                new Scale(-1000, 0.1F, 0), //throttle scale (ax + b)
                new Scale(-1500, 0.0000008F, 0, 0, 0), //Axes Scale (ax3 + bx2 + cx + d)
                1); //RadioSensitivityFactor
        }

        private static float GetGyroFactor()
        {
            return 287.5F;
        }
    }
}
