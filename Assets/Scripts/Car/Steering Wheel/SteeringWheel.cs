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

        public void SetSteeringWheelValue(float steeringWheelValue)
        {
            foreach(WheelCollider wheel in _carWheels)
            {
                wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steeringWheelValue * 45f, Time.deltaTime * 10f);
            }
        }
    }
}