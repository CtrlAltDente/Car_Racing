using Cars_Racing.Calculations.Interfaces;
using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    public class WheelsForwardShiftness : MonoBehaviour, IByCarSpeedValue
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private AnimationCurve _forwardShiftnessValueBySpeed;

        public float ByCarSpeedValue => _forwardShiftnessValueBySpeed.Evaluate(_wheels.GetSpeed() / CarConfiguration.EngineConfiguration.MaxSpeed);

        private void Update()
        {
            ConfigureShiftnessValue();
        }

        private void ConfigureShiftnessValue()
        {
            _wheels.DoWheelAction(wheel => SetShiftnessValue(wheel, ByCarSpeedValue));
        }

        private void SetShiftnessValue(WheelCollider wheelCollider, float shiftnessValue)
        {
            var forwardFriction = wheelCollider.forwardFriction;
            forwardFriction.stiffness = shiftnessValue;
            wheelCollider.forwardFriction = forwardFriction;
        }
    }
}