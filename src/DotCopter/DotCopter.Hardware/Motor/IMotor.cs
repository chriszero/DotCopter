namespace DotCopter.Hardware.Motor
{
    public interface IMotor
    {
        void Update(float throttle);
        void SetSafe();
    }
}