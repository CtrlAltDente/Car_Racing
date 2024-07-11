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
        [TextArea]
        private string _information;

        public float CurrentSpeed { get { return _wheelColliders.GetSpeed(); } }
        public float CurrentGearMinSpeed => CarConfigurationInfo.AverageGearSpeed * (_gearbox.CurrentGear - 2);
        public float CurrentGearMaxSpeed => CarConfigurationInfo.AverageGearSpeed * _gearbox.CurrentGear;

        public float PotentialRPM { get; private set; }
        public float CurrentRPM { get; private set; }

        public float MotorTorgue { get; private set; }

        public void SetEngineValues(float gasValue, float footBreakValue)
        {
            Gas(gasValue);
            FootBreak(footBreakValue);
        }

        private void Gas(float gasValue)
        {
            if (_gearbox.IsGearSwitching || _gearbox.IsNeutralGear)
            {
                gasValue = 0;
            }

            CalculateRPM(gasValue);

            MotorTorgue = CalculateMotorTorque(gasValue);

            _wheelColliders.DoWheelAction(wheel => wheel.motorTorque = MotorTorgue);

            _information = $"Speed: {CurrentSpeed}, Current RPM: {CurrentRPM.ToString("#")}, Potential RPM: {PotentialRPM.ToString("#")}, Motor Toqgue: {MotorTorgue.ToString("#")}";
        }

        private void FootBreak(float breakValue)
        {
            _wheelColliders.DoWheelAction(wheel => wheel.brakeTorque = breakValue * 300);
        }

        private void CalculateRPM(float gasValue)
        {
            PotentialRPM = Mathf.Clamp(Mathf.Lerp(0, CarConfigurationInfo.CarConfiguration.MaxRPM, CalculatePotentialRpm()), CarConfigurationInfo.CarConfiguration.MinRPM, CarConfigurationInfo.CarConfiguration.MaxRPM);

            float stepRpm = CarConfigurationInfo.CarConfiguration.MaxRPM * Time.deltaTime;
            CurrentRPM = Mathf.Clamp(CurrentRPM - stepRpm + 2 * stepRpm * Mathf.Abs(gasValue), CarConfigurationInfo.CarConfiguration.MinRPM, PotentialRPM);
        }

        private float CalculatePotentialRpm()
        {
            float potentialGearRpm = 0;

            if (!_gearbox.IsNeutralGear)
            {
                if (CurrentSpeed < CurrentGearMinSpeed)
                {
                    potentialGearRpm = 0;
                }
                else if (CurrentSpeed > CurrentGearMaxSpeed)
                {
                    potentialGearRpm = 1;
                }
                else
                {
                    potentialGearRpm = Mathf.Lerp(0, 1, (CurrentSpeed - CurrentGearMinSpeed) / (CurrentGearMaxSpeed - CurrentGearMinSpeed));
                }
            }

            return potentialGearRpm;
        }

        private float CalculateMotorTorque(float gasValue)
        {
            float rpmGearPower = (CarConstants.RPMPower.Evaluate(CurrentRPM / CarConfigurationInfo.CarConfiguration.MaxRPM) + CarConstants.RPMPowerToGearSpeedCurve.Evaluate(CurrentRPM / CarConfigurationInfo.CarConfiguration.MaxRPM)) * CalculatePotentialRpm();

            float gearMultiplier = CarConstants.MotorTorgueGearCurve.Evaluate(Mathf.Abs(_gearbox.CurrentGear) / CarConfigurationInfo.CarConfiguration.TopGear);

            float torque = (CarConfigurationInfo.CarConfiguration.HorsePower * 745.7f) / ((2 * Mathf.PI * CurrentRPM) / 60);

            float motorTorque = gasValue * rpmGearPower * torque;

            return motorTorque;
        }
    }
}