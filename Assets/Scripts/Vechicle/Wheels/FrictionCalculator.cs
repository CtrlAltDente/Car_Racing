using Cars_Racing.Calculations.Interfaces;
using Cars_Racing.Vehicle.Car;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    [Serializable]
    public struct FrictionCalculator : IByCarSpeedValue<WheelFrictionCurve>
    {
        [HideInInspector]
        public CarConfiguration CarConfiguration;
        [HideInInspector]
        public WheelCollider[] Wheels;

        public AnimationCurve ExtremumSlipCurve;
        public AnimationCurve ExtremumValueCurve;
        public AnimationCurve AsymptoteSlipCurve;
        public AnimationCurve AsymptoteValueCurve;
        public AnimationCurve ShiftnessCurve;

        public WheelFrictionCurve ByCarSpeedValue => CalculateValue();

        private WheelFrictionCurve CalculateValue()
        {
            float speedValue = Wheels.GetSpeed() / CarConfiguration.EngineConfiguration.MaxSpeed;

            return new WheelFrictionCurve()
            {
                extremumSlip = ExtremumSlipCurve.Evaluate(speedValue),
                extremumValue = ExtremumValueCurve.Evaluate(speedValue),
                asymptoteSlip = AsymptoteSlipCurve.Evaluate(speedValue),
                asymptoteValue = AsymptoteValueCurve.Evaluate(speedValue),
                stiffness = ShiftnessCurve.Evaluate(speedValue)
            };
        }
    }
}