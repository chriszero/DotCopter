using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Gyro;

namespace DotCopter.FlightController.TestHarness
{
    public class TestGyro : IGyro
    {
        private AircraftPrincipalAxes _axes;
        private readonly Random _random;
        public TestGyro()
        {
            _random = new Random();
            _axes = new AircraftPrincipalAxes {Pitch = 1, Roll = 1, Yaw = 1};
        }

        public AircraftPrincipalAxes Axes
        {
            get { return _axes; }
            set { _axes = value; }
        }

        public void Update()
        {
            _axes.Pitch = _random.Next();
            _axes.Roll = _random.Next();
            _axes.Yaw = _random.Next();   
        }
    }
}