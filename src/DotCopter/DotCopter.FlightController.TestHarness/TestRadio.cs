using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController.TestHarness
{
    public class TestRadio : IRadio
    {
        private readonly AircraftPrincipalAxes _axes;
        private readonly Random _random;

        public TestRadio()
        {
            _axes = new AircraftPrincipalAxes(1,1,1);
            _random = new Random();
        }

        public float Throttle
        {
            get { return 1f; }
        }

        public bool Gear
        {
            get { return true; }
        }

        public AircraftPrincipalAxes Axes
        {
            get { return _axes; }
        }

        public void Update()
        {
            _axes.Update(_random.Next(), _random.Next(), _random.Next());   
        }
    }
}