using System;
using DotCopter.Avionics;
using DotCopter.Hardware;

namespace DotCopter.Hardware.Implementations.Gyro
{
    public class DefaultGyro : Hardware.Gyro.Gyro
    {
        public DefaultGyro(AircraftPrincipalAxes axes) : base(axes)
        {
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }


        public override void Zero()
        {
            throw new NotImplementedException();
        }
    }
}
