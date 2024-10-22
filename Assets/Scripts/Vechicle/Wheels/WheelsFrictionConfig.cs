using Cars_Racing.Calculations.Interfaces;
using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    public class WheelsFrictionConfig : MonoBehaviour
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private WheelCollider[] _wheels;

        [SerializeField]
        private FrictionCalculator _forwardFrictionCalculator;
        [SerializeField]
        private FrictionCalculator _sidewaysFrictionCalculator;

        private void Awake()
        {
            _forwardFrictionCalculator.CarConfiguration = CarConfiguration;
            _forwardFrictionCalculator.Wheels = _wheels;

            _sidewaysFrictionCalculator.CarConfiguration = CarConfiguration;
            _sidewaysFrictionCalculator.Wheels = _wheels;
        }

        private void Update()
        {
            ConfigureWheelsFrictionValue();
        }

        private void ConfigureWheelsFrictionValue()
        {
            _wheels.DoWheelAction(wheel =>
            {
                wheel.forwardFriction = _forwardFrictionCalculator.ByCarSpeedValue;
                wheel.sidewaysFriction = _sidewaysFrictionCalculator.ByCarSpeedValue;
            });
        }
    }
}