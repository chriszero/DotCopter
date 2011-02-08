namespace DotCopter.Avionics
{
    public class Thrust
    {
        private int throttle;

        public Thrust(int throttle)
        {
            this.throttle = throttle;
        }

        public int Throttle
        {
            get { return throttle; }
        }
    }
}
