
using DotCopter.Avionics;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.FlightController
{
    public class AxesController
    {
        private readonly ControlAlgorithms.PID.PID _pitchController;
        private readonly ControlAlgorithms.PID.PID _rollController;
        private readonly ControlAlgorithms.PID.PID _yawController;

        public AxesController(PIDSettings pitchSettings, PIDSettings rollSettings, PIDSettings yawSettings, bool useWindup)
        {
            if (useWindup)
            {
                _pitchController = new PIDWindup(pitchSettings);
                _rollController = new PIDWindup(rollSettings);
                _yawController = new PIDWindup(yawSettings);
            }
            else
            {
                _pitchController = new ControlAlgorithms.Implementations.PID.PID(pitchSettings);
                _rollController = new ControlAlgorithms.Implementations.PID.PID(rollSettings);
                _yawController = new ControlAlgorithms.Implementations.PID.PID(yawSettings);
            }
            Axes = new AircraftPrincipalAxes(){Pitch = 0, Roll = 0, Yaw = 0};
        }

        public void Update(AircraftPrincipalAxes radio, AircraftPrincipalAxes gyro, float deltaTimeGain)
        {
            _pitchController.Update(radio.Pitch, gyro.Pitch, deltaTimeGain);
            _rollController.Update(radio.Roll, gyro.Roll, deltaTimeGain);
            _yawController.Update(radio.Yaw, gyro.Yaw, deltaTimeGain);
            Axes.Pitch = _pitchController.Output;
            Axes.Roll = _rollController.Output;
            Axes.Yaw = _yawController.Output;
        }

        public AircraftPrincipalAxes Axes;

    }
}
