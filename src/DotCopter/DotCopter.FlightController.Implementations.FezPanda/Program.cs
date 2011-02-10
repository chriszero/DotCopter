using System.IO;
using DotCopter.Commons.Logging;
using DotCopter.Commons.Serialization;
using DotCopter.Commons.Utilities;
using DotCopter.ControlAlgorithms.Implementations.Mixing;
using DotCopter.ControlAlgorithms.Mixing;
using DotCopter.ControlAlgorithms.PID;
using DotCopter.Hardware.Gyro;
using DotCopter.Hardware.Implementations.Bus;
using DotCopter.Hardware.Implementations.GHIElectronics.FEZPanda;
using DotCopter.Hardware.Implementations.GHIElectronics.Storage;
using DotCopter.Hardware.Implementations.Gyro;
using DotCopter.Hardware.Implementations.Motor;
using DotCopter.Hardware.Implementations.Radio;
using DotCopter.Hardware.Implementations.Storage;
using DotCopter.Hardware.Motor;
using DotCopter.Hardware.Radio;
using DotCopter.Hardware.Storage;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;
using PWM = DotCopter.Framework.Implementations.GHIElectronics.PWM.PWM;

namespace DotCopter.FlightController.Implementations.FezPanda
{
    public class Program
    {
        public static void Main()
        {
            //Initialize motors first
            MotorSettings motorSettings = GetMotorSettings();
            Motor frontMotor = new PWMMotor(new PWM((Cpu.Pin) FezPin.PWM.Di8, 4000000), motorSettings);
            Motor rearMotor = new PWMMotor(new PWM((Cpu.Pin) FezPin.PWM.Di10, 4000000), motorSettings);
            Motor leftMotor = new PWMMotor(new PWM((Cpu.Pin) FezPin.PWM.Di9, 4000000), motorSettings);
            Motor rightMotor = new PWMMotor(new PWM((Cpu.Pin) FezPin.PWM.Di6, 4000000), motorSettings);
            MotorMixer mixer = new QuadMixer(frontMotor, rearMotor, leftMotor, rightMotor);

            //Telemetry
            ISDCard sdCard = (ISDCard) new SDCard();
            if (Configuration.DebugInterface.GetCurrent() == Configuration.DebugInterface.Port.USB1)
                sdCard.MountFileSystem();
            else
                new TelemetryPresenter(sdCard,(Cpu.Pin) FezPin.Digital.LED);
            ILogger logger = new PersistenceWriter(new FileStream(@"\SD\telemetry.bin",FileMode.OpenOrCreate), new TelemetryFormatter());

            //Sensors
            I2CBus twiBus = new I2CBus();
            Gyro gyro = new ITG3200(twiBus, GetGyroFactor());
            Radio radio = new DefaultRadio(twiBus, GetRadioSettings());

            //Control Algoriths
            PIDSettings[] pidSettings = GetPIDSettings();
            AxesController axesController = new AxesController(pidSettings[0], pidSettings[1], pidSettings[2], true);
            ControllerLoopSettings loopSettings = GetLoopSettings();
            Controller controller = new Controller(mixer, axesController, gyro, radio, loopSettings, GetMotorSettings(),logger);

            //controller.Start();
        }

        private static PIDSettings[] GetPIDSettings()
        {

            PIDSettings pitchSettings = new PIDSettings
                                            {
                                                ProportionalGain = 3.2F,
                                                IntegralGain = 0F,
                                                DerivativeGain = -.5F,
                                                WindupLimit = 2000F
                                            };

            PIDSettings rollSettings = new PIDSettings
                                           {
                                               ProportionalGain = 3.2F,
                                               IntegralGain = 0F,
                                               DerivativeGain = -.5F,
                                               WindupLimit = 2000F
                                           };

            PIDSettings yawSettings = new PIDSettings
                                          {
                                              ProportionalGain = 3.2F,
                                              IntegralGain = 0F,
                                              DerivativeGain = -.5F,
                                              WindupLimit = 2000F
                                          };
            return new[] { pitchSettings, rollSettings, yawSettings };
        }

        private static ControllerLoopSettings GetLoopSettings()
        {
            return new ControllerLoopSettings(
                50,         //radioLoopFrequency
                150,        //sensorLoopFrequency
                150,        //controlAlgorithmFrequency
                150,        //motorLoopFrequency
                1,          //telemetryLoopFrequency
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