using DotCopter.Avionics;

namespace DotCopter.ControlAlgorithms.Mixing
{
    public interface IMotorMixer
    {
        void SetSafe();
        void Update(float throttle, AircraftPrincipalAxes output);
    }
}