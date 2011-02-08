using DotCopter.Avionics;

namespace DotCopter.Hardware.Gyro
{
    public interface IGyro
    {
        AircraftPrincipalAxes Axes { get; }
        void Update();
    }
}
