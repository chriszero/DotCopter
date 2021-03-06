﻿using System.IO;
using DotCopter.Avionics;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.GHIElectronics.Storage;
using DotCopter.Hardware.Implementations.Gyro;
using DotCopter.Hardware.Implementations.Radio;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController.Implementations.Netduino
{
    public class Program
    {
        public static void Main()
        {
            SDCard sdCard = new SDCard();
            //todo: need sdCard mode detection
            ILogger logger = new PersistenceWriter(new FileStream(@"\SD\telemetry.bin", FileMode.CreateNew), new TelemetryFormatter());
            //ILogger logger = new DebugLogger(); // use for debugging
            //ILogger logger = new NullLogger(); // use for release

            //Assumes one type of speed controller on quad
            //ISpeedControllerSettings speedControllerSettings = new HobbyKingSs2530Settings();
            
            //IPWM pwmFront = new SecretLabsPWM(Pins.GPIO_PIN_D9);
            //SpeedController electricSpeedControllerFront = new SpeedController(speedControllerSettings, pwmFront);

            //IPWM pwmRear = new SecretLabsPWM(Pins.GPIO_PIN_D8);
            //SpeedController electricSpeedControllerRear = new SpeedController(speedControllerSettings, pwmRear);

            //IPWM pwmRight = new SecretLabsPWM(Pins.GPIO_PIN_D8);
            //SpeedController electricSpeedControllerRight = new SpeedController(speedControllerSettings, pwmRight);

            //IPWM pwmLeft = new SecretLabsPWM(Pins.GPIO_PIN_D8);
            //SpeedController electricSpeedControllerLeft = new SpeedController(speedControllerSettings, pwmLeft);


            MotorMixer mixer = null;
            PIDSettings[] pidSettings = GetPIDSettings();
            AxesController axesController = new AxesController(pidSettings[0], pidSettings[1], pidSettings[2], true);
            Gyro gyro = new DefaultGyro(new AircraftPrincipalAxes());
            Radio radio = new DefaultRadio(null,null);
            ControllerLoopSettings loopSettings = GetLoopSettings();
            Controller controller = new Controller(mixer, axesController, gyro, radio, loopSettings, GetMotorSettings(), logger);


        }

        private static PIDSettings[] GetPIDSettings()
        {

            PIDSettings pitchSettings = new PIDSettings
            {
                DerivativeGain = 3.2F,
                IntegralGain = 0F,
                ProportionalGain = -.5F,
                WindupLimit = 2000F
            };

            PIDSettings rollSettings = new PIDSettings
            {
                DerivativeGain = 3.2F,
                IntegralGain = 0F,
                ProportionalGain = -.5F,
                WindupLimit = 2000F
            };

            PIDSettings yawSettings = new PIDSettings
            {
                DerivativeGain = 3.2F,
                IntegralGain = 0F,
                ProportionalGain = -.5F,
                WindupLimit = 2000F
            };
            return new[] { pitchSettings, rollSettings, yawSettings };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(25, 300, 300, 300, 2, 10000000);
        }

        private static MotorSettings GetMotorSettings()
        {
            return new MotorSettings(new Scale(0,0),0,15,95); 
        }
    }
}
