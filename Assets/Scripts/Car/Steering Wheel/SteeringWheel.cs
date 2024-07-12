using Cars_Racing.Vehicle.Wheels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.SteeringWheelLogic
{
    public class SteeringWheel : MonoBehaviour
    {
        [SerializeField]
        private WheelCollider[] _carWheels;

        [SerializeField]
        private float _rotationAngle = 45f;
        [SerializeField]
        private float _rotationSpeed = 90f;

        public float SpeedOnWheelAmplifying => Mathf.Clamp(1 - _carWheels.GetSpeed() / Car.CarConfigurationInfo.CarConfiguration.MaxSpeed, 0.01f, 1);

        public void SetSteeringWheelValue(float steeringWheelValue)
        {
            foreach (WheelCollider wheel in _carWheels)
            {
                wheel.steerAngle = Mathf.MoveTowards(wheel.steerAngle, SpeedOnWheelAmplifying * steeringWheelValue * _rotationAngle, Time.deltaTime * SpeedOnWheelAmplifying * _rotationSpeed);
            }

            Debug.Log((_carWheels[0].steerAngle).ToString("#"));
        }
    }
}