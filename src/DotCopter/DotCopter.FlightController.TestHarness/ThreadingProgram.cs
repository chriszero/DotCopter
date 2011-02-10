using System;
using System.IO;
using System.Threading;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Implementations.Mixing;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using Microsoft.SPOT;

namespace DotCopter.FlightController.TestHarness
{
    public class ThreadingProgram
    {
        private long _lastSensorTime = DateTime.Now.Ticks;
        private readonly ControllerLoopSettings _loopSettings;
        public ThreadingProgram()
        {
            IGyro gyro = new TestGyro();
            Sensors sensors = new Sensors(gyro);

            IMotor testMotor = new TestMotor();
            IMotorMixer mixer = new QuadMixer(testMotor, testMotor, testMotor, testMotor);
            ILogger logger = new PersistenceWriter(new MemoryStream(), new TelemetryFormatter());

            IRadio radio = new TestRadio();
            ProportionalIntegralDerivativeSettings[] pidSettings = GetPIDSettings();
            AxesController axesController = new AxesController(pidSettings[0], pidSettings[1], pidSettings[2], true);
            _loopSettings = GetLoopSettings();

            
            Timer sensorTimer = new Timer(tester, sensors,new TimeSpan(0),new TimeSpan((long)_loopSettings.SensorLoopFrequency) );
            TestController controller = new TestController(mixer, axesController, gyro, radio, _loopSettings,
                                                           GetMotorSettings(), logger);

        }
        private void tester(object state)
        {
            Debug.Print((_loopSettings.LoopUnit / (DateTime.Now.Ticks - _lastSensorTime)).ToString());
            _lastSensorTime = DateTime.Now.Ticks;
            Sensors sensors = (Sensors)state;
            sensors.ReadGyro();
            //Debug.Print(DateTime.Now.Ticks.ToString());
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
                200,         //radioLoopFrequency
                200,        //sensorLoopFrequency
                200,        //controlAlgorithmFrequency
                200,        //motorLoopFrequency
                200,          //telemetryLoopFrequency
                10000000);  //loopUnit
        }

        private static MotorSettings GetMotorSettings()
        {
            return new MotorSettings(
                new Scale(0, 10000, 1000000), //motor scale
                0,                      //safeOutput
                15,                     //minimumOutput
                95);                    //maximimOutput
        }

        private static RadioSettings GetRadioSettings()
        {
            return new RadioSettings(
                new Scale(-1000, 0.1F, 0), //throttle scale (ax + b)
                new Scale(-1500, 0.0000008F, 0, 0, 0), //Axes Scale (ax3 + bx2 + cx + d)
                0.5F); //RadioSensitivityFactor
        }

        private static float GetGyroFactor()
        {
            return 287.5F;
        }
    }
}