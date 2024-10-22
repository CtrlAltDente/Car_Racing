using Cars_Racing.Calculations.Interfaces;
using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    public class WheelsSidewaysShiftness : MonoBehaviour, IByCarSpeedValue
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private AnimationCurve _sidewaysShiftnessValueBySpeed;

        public float ByCarSpeedValue => _sidewaysShiftnessValueBySpeed.Evaluate(_wheels.GetSpeed() / CarConfiguration.EngineConfiguration.MaxSpeed);

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
            var sidewaysFriction = wheelCollider.sidewaysFriction;
            sidewaysFriction.stiffness = shiftnessValue;
            wheelCollider.sidewaysFriction = sidewaysFriction;
        }
    }
}