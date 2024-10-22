using Cars_Racing.Calculations.Interfaces;
using Cars_Racing.Vehicle.Car;
using Cars_Racing.Vehicle.Wheels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.SteeringWheelLogic
{
    public class SteeringWheel : MonoBehaviour, IByCarSpeedValue<float>
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _carWheels;

        [SerializeField]
        private float _rotationAngle = 45f;
        [SerializeField]
        private float _rotationSpeed = 90f;

        [SerializeField]
        private AnimationCurve _steeringWheelPowerBySpeed;

        public float ByCarSpeedValue => _steeringWheelPowerBySpeed.Evaluate(_carWheels.GetSpeed() / CarConfiguration.EngineConfiguration.MaxSpeed);

        public void SetSteeringWheelValue(float steeringWheelValue)
        {
            foreach (WheelCollider wheel in _carWheels)
            {
                wheel.steerAngle = Mathf.MoveTowards(wheel.steerAngle, ByCarSpeedValue * steeringWheelValue * _rotationAngle, Time.deltaTime * ByCarSpeedValue * _rotationSpeed);
            }

            Debug.Log((_carWheels[0].steerAngle).ToString("#"));
        }
    }
}