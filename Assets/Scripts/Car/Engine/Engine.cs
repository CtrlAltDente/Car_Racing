using Cars_Racing.Vehicle.Car;
using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.EngineLogic
{
    public class Engine : MonoBehaviour
    {
        [SerializeField]
        private Gearbox _gearbox;

        [SerializeField]
        private WheelCollider[] _wheelColliders;

        [SerializeField]
        private string _information;

        public float CurrentRPM { get; private set; }
        public float CurrentSpeed { get { return _wheelColliders.GetSpeed(); } }
        public float MotorTorgue { get; private set; }

        public void SetEngineValues(float gasValue, float footBreakValue)
        {
            Gas(gasValue);
            FootBreak(footBreakValue);
        }

        private void Gas(float gasValue)
        {
            MotorTorgue = 0;

            if (!_gearbox.IsGearSwitching && !_gearbox.IsNeutralGear && gasValue != 0)
            {

            }
            else
            {
                gasValue = 0;
            }

            MotorTorgue = CalculateMotorTorque(gasValue);

            _wheelColliders.DoWheelAction(wheel => wheel.motorTorque = MotorTorgue);

            _information = $"Speed: {CurrentSpeed}, RPM: {CurrentRPM}, Motor Toqgue: {MotorTorgue}";
        }

        private void FootBreak(float breakValue)
        {
            breakValue = 2;
        }

        private float CalculateMotorTorque(float gasValue)
        {
            float motorTorque = 0;

            float avgGearSpeed = CarConfigurationInfo.AverageGearSpeed;
            float currentGearMinSpeed = !_gearbox.IsNeutralGear ? (avgGearSpeed * (_gearbox.CurrentGear - 2) * 1.2f) : 0;
            float currentGearMaxSpeed = !_gearbox.IsNeutralGear ? (avgGearSpeed * _gearbox.CurrentGear * 1.2f) : 0;
            float currentGearRPM = !_gearbox.IsNeutralGear ? Mathf.Lerp(0, 1f, (CurrentSpeed - currentGearMinSpeed) / (currentGearMaxSpeed - currentGearMinSpeed)) : 0;
            float rpmGearPower = CarConstants.RPMPowerToGearSpeedCurve.Evaluate(CurrentRPM / CarConfigurationInfo.CarConfiguration.MaxRPM);

            CurrentRPM = Mathf.Clamp(Mathf.Lerp(0, CarConfigurationInfo.CarConfiguration.MaxRPM, currentGearRPM), CarConfigurationInfo.CarConfiguration.MinRPM, CarConfigurationInfo.CarConfiguration.MaxRPM);

            float gearMultiplier = CarConstants.MotorTorgueGearCurve.Evaluate(Mathf.Abs(_gearbox.CurrentGear) / CarConfigurationInfo.CarConfiguration.TopGear);

            float torque = (CarConfigurationInfo.CarConfiguration.HorsePower * 745.7f) / ((2 * Mathf.PI * CurrentRPM) / 60);

            motorTorque = gasValue * rpmGearPower * torque * gearMultiplier * Mathf.Lerp(0, 1, currentGearRPM);

            return motorTorque;
        }
    }
}