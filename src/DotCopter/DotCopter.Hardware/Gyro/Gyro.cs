using DotCopter.Avionics;

namespace DotCopter.Hardware.Gyro
{
    public abstract class Gyro
    {
        public AircraftPrincipalAxes Axes;
        public abstract void Update();
        public abstract void Zero();

        public Gyro(AircraftPrincipalAxes axes)
        {
            Axes = axes;
        }
    }
}
