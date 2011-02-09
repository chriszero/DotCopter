using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController.TestHarness
{
    public class TestRadio : IRadio
    {
        private AircraftPrincipalAxes _axes;
        private readonly Random _random;

        public TestRadio()
        {
            _axes = new AircraftPrincipalAxes { Pitch = 1, Roll = 1, Yaw = 1 };
            _random = new Random();
        }

        public float Throttle
        {
            get { return 16f; }
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
            _axes.Pitch = _random.Next();
            _axes.Roll = _random.Next();
            _axes.Yaw = _random.Next();   
        }
    }
}