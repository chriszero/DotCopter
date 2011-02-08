namespace DotCopter.Avionics
{
    public class AircraftPrincipalAxes
    {
        public AircraftPrincipalAxes(float pitch, float roll, float yaw)
        {
            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
        }

        public float Pitch { get; private set; }

        public float Roll { get; private set; }

        public float Yaw { get; private set; }

        public void Update(float pitch, float roll, float yaw)
        {
            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
        }
    }
}