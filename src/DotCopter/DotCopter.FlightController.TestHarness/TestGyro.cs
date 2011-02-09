using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Gyro;

namespace DotCopter.FlightController.TestHarness
{
    public class TestGyro : IGyro
    {
        private readonly AircraftPrincipalAxes _axes;
        private readonly Random _random;
        public TestGyro()
        {
            _random = new Random();
            _axes = new AircraftPrincipalAxes(1,1,1);
        }

        public AircraftPrincipalAxes Axes
        {
            get { return _axes; }
        }

        public void Update()
        {
            _axes.Update(_random.Next(), _random.Next(),_random.Next());   
        }
    }
}