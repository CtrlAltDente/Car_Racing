using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    public class WheelsForwardShiftness : MonoBehaviour
    {
        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private float _defaultValue = 1f;
        [SerializeField]
        private float _targetValue = 1.8f;

        private float ShiftnessValue => Mathf.Lerp(_defaultValue, _targetValue, _wheels.GetSpeed() / (Car.CarConfigurationInfo.CarConfiguration.MaxSpeed));

        private void Update()
        {
            ConfigureShiftnessValue();
        }

        private void ConfigureShiftnessValue()
        {
            _wheels.DoWheelAction(wheel => SetShiftnessValue(wheel, ShiftnessValue));
        }

        private void SetShiftnessValue(WheelCollider wheelCollider, float shiftnessValue)
        {
            var forwardFriction = wheelCollider.forwardFriction;
            forwardFriction.stiffness = shiftnessValue;
            wheelCollider.forwardFriction = forwardFriction;
        }
    }
}