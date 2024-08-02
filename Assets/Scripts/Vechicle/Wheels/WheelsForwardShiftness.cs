using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    public class WheelsForwardShiftness : MonoBehaviour
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private float _defaultValue = 1f;
        [SerializeField]
        private float _targetValue = 1.8f;

        private float ShiftnessValue => Mathf.Lerp(_defaultValue, _targetValue, _wheels.GetSpeed() / (CarConfiguration.EngineConfiguration.MaxSpeed));

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