namespace DotCopter.FlightController
{
    public class ControllerLoopSettings
    {

        public ControllerLoopSettings(int radioLoopFrequency, int sensorLoopFrequency, int controlAlgorithmLoopFrequency, int motorLoopFrequency, int telemetryLoopFrequency, int loopUnit)
        {
            RadioLoopPeriod = loopUnit / (RadioLoopFrequency = radioLoopFrequency);
            SensorLoopPeriod = loopUnit / (SensorLoopFrequency = sensorLoopFrequency);
            ControlAlgorithmPeriod = loopUnit / (ControlAlgorithmLoopFrequency = controlAlgorithmLoopFrequency);
            MotorLoopPeriod = loopUnit / (MotorLoopFrequency = motorLoopFrequency);
            TelemetryLoopPeriod = loopUnit / (TelemetryLoopFrequency = telemetryLoopFrequency);
            LoopUnit = loopUnit;
        }

        public int RadioLoopFrequency { get; private set; }
        public int RadioLoopPeriod { get; private set; }

        public int SensorLoopFrequency { get; private set; }
        public int SensorLoopPeriod { get; private set; }

        public int ControlAlgorithmLoopFrequency { get; private set; }
        public int ControlAlgorithmPeriod { get; private set; }

        public int MotorLoopFrequency { get; private set; }
        public int MotorLoopPeriod { get; private set; }

        public int TelemetryLoopFrequency { get; private set; }
        public int TelemetryLoopPeriod { get; private set; }

        public int LoopUnit { get; private set; }
    }
}
