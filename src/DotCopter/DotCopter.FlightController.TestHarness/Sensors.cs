using DotCopter.Avionics;
using DotCopter.Hardware.Gyro;

namespace DotCopter.FlightController.TestHarness
{
    public class Sensors
    {
        private readonly IGyro _gyro;

        public Sensors(IGyro gyro )
        {
            _gyro = gyro;
        }

        public AircraftPrincipalAxes ReadGyro()
        {
            _gyro.Update();
            return _gyro.Axes;

        }
    }
}
