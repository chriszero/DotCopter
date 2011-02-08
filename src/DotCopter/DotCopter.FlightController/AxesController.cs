using DotCopter.Avionics;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.FlightController
{
    public class AxesController
    {
        private readonly IProportionalIntegralDerivative _pitchController;
        private readonly IProportionalIntegralDerivative _rollController;
        private readonly IProportionalIntegralDerivative _yawController;

        public AxesController(ProportionalIntegralDerivativeSettings pitchSettings, ProportionalIntegralDerivativeSettings rollSettings, ProportionalIntegralDerivativeSettings yawSettings, bool useWindup)
        {
            if (useWindup)
            {
                _pitchController = new ProportionalIntegralDerivativeWithWindupGuard(pitchSettings);
                _rollController = new ProportionalIntegralDerivativeWithWindupGuard(rollSettings);
                _yawController = new ProportionalIntegralDerivativeWithWindupGuard(yawSettings);
            }
            else
            {
                _pitchController = new ProportionalIntegralDerivative(pitchSettings);
                _rollController = new ProportionalIntegralDerivative(rollSettings);
                _yawController = new ProportionalIntegralDerivative(yawSettings);
            }
            Axes = new AircraftPrincipalAxes(0,0,0);
        }

        public void Update(AircraftPrincipalAxes radio, AircraftPrincipalAxes gyro, float deltaTimeGain)
        {
            _pitchController.Update(radio.Pitch, gyro.Pitch, deltaTimeGain);
            _rollController.Update(radio.Roll, gyro.Roll, deltaTimeGain);
            _yawController.Update(radio.Yaw, gyro.Yaw, deltaTimeGain);
            Axes.Update(_pitchController.Output,
                        _rollController.Output,
                        _yawController.Output);
        }

        public AircraftPrincipalAxes Axes { get; private set; }

    }
}
