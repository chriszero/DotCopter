using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Gyro;

namespace DotCopter.Hardware.Implementations.Gyro
{
    public class DefaultGyro: IGyro
    {
        public AircraftPrincipalAxes Axes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }


        public void Zero()
        {
            throw new NotImplementedException();
        }
    }
}
