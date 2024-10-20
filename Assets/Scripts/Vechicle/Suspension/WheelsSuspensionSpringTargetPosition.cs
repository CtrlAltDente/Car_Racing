using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Suspension
{
    public class WheelsSuspensionSpringTargetPosition : MonoBehaviour
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private float _defaultValue;
        [SerializeField]
        private float _targetValue;

        private float SpringTargetValue => Mathf.Lerp(_defaultValue, _targetValue, _wheels.GetSpeed() / (CarConfiguration.EngineConfiguration.MaxSpeed / 2));

        private void Update()
        {
            ConfigureTargetValue();
        }

        private void ConfigureTargetValue()
        {
            _wheels.DoWheelAction(wheel => SetSuspensionSpringTargetValue(wheel, SpringTargetValue));
        }

        private void SetSuspensionSpringTargetValue(WheelCollider wheelCollider, float targetPositionValue)
        {
            var suspensionSpring = wheelCollider.suspensionSpring;
            suspensionSpring.targetPosition = targetPositionValue;
            wheelCollider.suspensionSpring = suspensionSpring;
        }
    }
}