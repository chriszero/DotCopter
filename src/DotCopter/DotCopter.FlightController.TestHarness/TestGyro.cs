using System;
using DotCopter.Avionics;
using DotCopter.Hardware.Gyro;

namespace DotCopter.FlightController.TestHarness
{
    public class TestGyro : Gyro
    {
        private readonly Random _random;
        public TestGyro() : base(new AircraftPrincipalAxes {Pitch = 0, Roll = 0, Yaw = 0})
        {
            _random = new Random();
        }
        
        public override void Update()
        {
            Axes.Pitch = _random.Next();
            Axes.Roll = _random.Next();
            Axes.Yaw = _random.Next();   
        }

        public override void Zero()
        {
            throw new NotImplementedException();
        }
    }
}