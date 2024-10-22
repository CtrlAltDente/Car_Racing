using Cars_Racing.Calculations.Interfaces;
using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Suspension
{
    public class WheelsSuspensionSpringTargetPosition : MonoBehaviour, IByCarSpeedValue<float>
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private AnimationCurve _suspensionValueBySpeed;

        public float ByCarSpeedValue => _suspensionValueBySpeed.Evaluate(_wheels.GetSpeed() / CarConfiguration.EngineConfiguration.MaxSpeed);

        private void Update()
        {
            ConfigureTargetValue();
        }

        private void ConfigureTargetValue()
        {
            _wheels.DoWheelAction(wheel => SetSuspensionSpringTargetValue(wheel, ByCarSpeedValue));
        }

        private void SetSuspensionSpringTargetValue(WheelCollider wheelCollider, float targetPositionValue)
        {
            var suspensionSpring = wheelCollider.suspensionSpring;
            suspensionSpring.targetPosition = targetPositionValue;
            wheelCollider.suspensionSpring = suspensionSpring;
        }
    }
}