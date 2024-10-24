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
        private Transmission.Transmission _transmission;


        [SerializeField]
        private WheelCollider[] _wheelColliders;

        [SerializeField]
        [TextArea]
        private string _information;

        public float CurrentSpeed { get { return _wheelColliders.GetSpeed(); } }

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
            float movement = Mathf.Clamp(_gearbox.GearNumber, -1, 1);

            float rpmGearPower = CarConstants.RPMPower.Evaluate(PotentialRPM / CarConfiguration.EngineConfiguration.MaxRPM);
            float rpmGearSpeedPower = CarConstants.RPMPowerToGearSpeedCurve.Evaluate(CalculateSpeedPower());

            float gearMultiplier = CarConstants.MotorTorqueGearCurve.Evaluate(Mathf.Abs(_gearbox.GearNumber) / CarConfiguration.GearboxConfiguration.TopGear);

            float torque = (CarConfiguration.EngineConfiguration.HorsePower * 745.7f) / ((2 * Mathf.PI * CurrentRPM) / 60); //It is a formula!

            PotentialMotorTorque = torque * rpmGearPower * rpmGearSpeedPower * gearMultiplier;

            float motorTorqueChangeSpeed = Mathf.Lerp(100, 200, CurrentRPM / CarConfiguration.EngineConfiguration.MaxRPM); //Need to rework!
            MotorTorgue = Mathf.MoveTowards(MotorTorgue, PotentialMotorTorque * gasValue * movement, motorTorqueChangeSpeed * Time.deltaTime);
        }

        private float CalculateSpeedPower()
        {
            if (_gearbox.IsNegativeGear)
            {
                return Mathf.Lerp(0, 1, Mathf.Abs(CurrentSpeed - 10) / ((Mathf.Abs(_transmission.CurrentGearMaxSpeed) - 0)));
            }
            else
            {
                return Mathf.Clamp01(Mathf.Lerp(0, 1, (CurrentSpeed - _transmission.CurrentGearMinSpeed) / (_transmission.CurrentGearMaxSpeed - _transmission.CurrentGearMinSpeed)));
            }
        }

        private void FootBreak(float breakValue)
        {
            _wheelColliders.DoWheelAction(wheel => wheel.brakeTorque = breakValue * 300);
        }
    }
}