using System;
using DotCopter.Avionics;
using DotCopter.Hardware;
using DotCopter.Hardware.Radio;

namespace DotCopter.FlightController.TestHarness
{
    public class TestRadio : Radio
    {
        private readonly Random _random;

        public TestRadio() : base(0, new AircraftPrincipalAxes { Pitch = 1, Roll = 1, Yaw = 1 },false)
        {
            _random = new Random();
        }

        public override void Update()
        {
            Axes.Pitch = _random.Next();
            Axes.Roll = _random.Next();
            Axes.Yaw = _random.Next();   
        }
    }
}