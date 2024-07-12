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
            if (_gearbox.IsGearSwitching)
            {
                gasValue = 0;
            }

            CalculateRPM(gasValue);

            MotorTorgue = CalculateMotorTorque(gasValue);

            _wheelColliders.DoWheelAction(wheel => wheel.motorTorque = MotorTorgue);

            _information = $"Speed: {CurrentSpeed}, Current RPM: {CurrentRPM.ToString("#")}, Potential RPM: {PotentialRPM.ToString("#")}, Motor Toqgue: {MotorTorgue.ToString("#")}, Speed Power: {CalculateSpeedPower()}";
        }

        private void CalculateRPM(float gasValue)
        {
            PotentialRPM = Mathf.Clamp(Mathf.Lerp(0, CarConfigurationInfo.CarConfiguration.MaxRPM, CalculateSpeedPower()), CarConfigurationInfo.CarConfiguration.MinRPM, CarConfigurationInfo.CarConfiguration.MaxRPM);

            float stepRpm = CarConfigurationInfo.CarConfiguration.MaxRPM * Time.deltaTime;
            float rpm = Mathf.MoveTowards(CurrentRPM, PotentialRPM, stepRpm);

            CurrentRPM = gasValue > 0 ? rpm : Mathf.Clamp(CurrentRPM - stepRpm + 2 * stepRpm * Mathf.Abs(gasValue), CarConfigurationInfo.CarConfiguration.MinRPM, CarConfigurationInfo.CarConfiguration.MaxRPM);
        }

        private float CalculateMotorTorque(float gasValue)
        {
            float rpmGearPower = CarConstants.RPMPower.Evaluate(CurrentRPM / CarConfigurationInfo.CarConfiguration.MaxRPM);
            float rpmGearSpeedPower = CarConstants.RPMPowerToGearSpeedCurve.Evaluate(CalculateSpeedPower());

            float torque = (CarConfigurationInfo.CarConfiguration.HorsePower * 745.7f) / ((2 * Mathf.PI * CurrentRPM) / 60);
            float movement = Mathf.Clamp(_gearbox.CurrentGear, -1, 1);

            float gearMultiplier = CarConstants.MotorTorgueGearCurve.Evaluate(Mathf.Abs(_gearbox.CurrentGear) / CarConfigurationInfo.CarConfiguration.TopGear);

            return gasValue * ((rpmGearPower + rpmGearSpeedPower)) * torque * (1 - CalculateSpeedPower()) * movement;
        }

        private float CalculateSpeedPower()
        {
            if (_gearbox.CurrentGear < 0)
            {
                return Mathf.Lerp(0, 1, Mathf.Abs(CurrentSpeed - 0) / ((Mathf.Abs(CurrentGearMaxSpeed) - 0)));
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