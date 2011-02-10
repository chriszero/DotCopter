namespace DotCopter.Hardware.Motor
{
    public abstract class Motor
    {
        public abstract void Update(float throttle);
        public abstract void SetSafe();
    }
}