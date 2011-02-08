namespace DotCopter.Framework.PulseWidthModulation
{
    public interface IPulseWidthModulation
    {
        void SetPulse(uint highTime);
        void SetPulse(uint period, uint highTime);
    }
}