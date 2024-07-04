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

        public void SetSteeringWheelValue(float steeringWheelValue)
        {
            foreach(WheelCollider wheel in _carWheels)
            {
                wheel.steerAngle = Mathf.MoveTowards(wheel.steerAngle, steeringWheelValue * _rotationAngle, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}