using Cars_Racing.Vehicle.Car;
using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.EngineLogic
{
    public class Engine : MonoBehaviour
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private Gearbox _gearbox;

        [SerializeField]
        private WheelCollider[] _wheelColliders;

        [SerializeField]
        [TextArea]
        private string _information;

        public float CurrentSpeed { get { return _wheelColliders.GetSpeed(); } }
        public float CurrentGearMinSpeed => CarConfiguration.AverageGearSpeed * (_gearbox.CurrentGear - 2);
        public float CurrentGearMaxSpeed => CarConfiguration.AverageGearSpeed * _gearbox.CurrentGear;

        public float PotentialRPM { get; private set; }
        public float CurrentRPM { get; private set; }

        public float PotentialMotorTorque { get; private set; }
        public float MotorTorgue { get; private set; }

        public void SetEngineValues(float gasValue, float footBreakValue)
        {
            Gas(gasValue);
            FootBreak(footBreakValue);
        }

        private void Gas(float gasValue)
        {
            if (_gearbox.IsGearSwitching)
            {
                gasValue = 0;
            }

            CalculateRPM(gasValue);
            CalculateMotorTorque(gasValue);

            _wheelColliders.DoWheelAction(wheel => wheel.motorTorque = MotorTorgue);

            _information = $"Speed: {CurrentSpeed}, Current RPM: {CurrentRPM.ToString("#")}, Potential RPM: {PotentialRPM.ToString("#")}, Motor Toqgue: {MotorTorgue.ToString("#")}, Speed Power: {CalculateSpeedPower()}";
        }

        private void CalculateRPM(float gasValue)
        {
            PotentialRPM = Mathf.Clamp(Mathf.Lerp(0, CarConfiguration.EngineConfiguration.MaxRPM, CalculateSpeedPower()), CarConfiguration.EngineConfiguration.MinRPM, CarConfiguration.EngineConfiguration.MaxRPM);

            float stepRpm = CarConfiguration.EngineConfiguration.MaxRPM * Time.deltaTime;
            float rpm = Mathf.MoveTowards(CurrentRPM, PotentialRPM, stepRpm);
            float rpmIncreaseCalculation = CurrentRPM - stepRpm + stepRpm * Mathf.Abs(gasValue);

            float targetRpmValue = gasValue > 0 ? rpm : Mathf.Clamp(rpmIncreaseCalculation, CarConfiguration.EngineConfiguration.MinRPM, CarConfiguration.EngineConfiguration.MaxRPM);

            CurrentRPM = targetRpmValue;
        }

        private void CalculateMotorTorque(float gasValue)
        {
            float movement = Mathf.Clamp(_gearbox.CurrentGear, -1, 1);

            float rpmGearPower = CarConstants.RPMPower.Evaluate(PotentialRPM / CarConfiguration.EngineConfiguration.MaxRPM);
            float rpmGearSpeedPower = CarConstants.RPMPowerToGearSpeedCurve.Evaluate(CalculateSpeedPower());

            float gearMultiplier = CarConstants.MotorTorqueGearCurve.Evaluate(Mathf.Abs(_gearbox.CurrentGear) / CarConfiguration.GearboxConfiguration.TopGear);

            float torque = (CarConfiguration.EngineConfiguration.HorsePower * 745.7f) / ((2 * Mathf.PI * CurrentRPM) / 60); //It is a formula!

            PotentialMotorTorque = torque * rpmGearPower * rpmGearSpeedPower * gearMultiplier;
            MotorTorgue = Mathf.MoveTowards(MotorTorgue, PotentialMotorTorque * gasValue * movement, CarConfiguration.EngineConfiguration.MaxSpeed * Time.deltaTime);
        }

        private float CalculateSpeedPower()
        {
            if (_gearbox.CurrentGear < 0)
            {
                return Mathf.Lerp(0, 1, Mathf.Abs(CurrentSpeed - 10) / ((Mathf.Abs(CurrentGearMaxSpeed) - 0)));
            }
            else if (CurrentSpeed < CurrentGearMinSpeed)
            {
                return 0;
            }
            else if (CurrentSpeed > CurrentGearMaxSpeed)
            {
                return 1;
            }
            else
            {
                return Mathf.Lerp(0, 1, (CurrentSpeed - CurrentGearMinSpeed) / (CurrentGearMaxSpeed - CurrentGearMinSpeed));
            }
        }

        private void FootBreak(float breakValue)
        {
            _wheelColliders.DoWheelAction(wheel => wheel.brakeTorque = breakValue * 300);
        }
    }
}