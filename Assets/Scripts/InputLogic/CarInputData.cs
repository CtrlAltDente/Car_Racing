using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.InputLogics
{
    [Serializable]
    public struct CarInputData
    {
        public float GasInput;
        public float FootBreakInput;
        public float SteeringWheelInput;
        public float HandBreakInput;
        public bool GearIncreaseInput;
        public bool GearDecreaseInput;

        public CarInputData(float gasInput, float footBreakInput, float steeringWheelInput, float handBreakInput, bool gearIncreaseInput, bool gearDecreaseInput)
        {
            GasInput = gasInput;
            FootBreakInput = footBreakInput;
            SteeringWheelInput = steeringWheelInput;
            HandBreakInput = handBreakInput;
            GearIncreaseInput = gearIncreaseInput;
            GearDecreaseInput = gearDecreaseInput;
        }
    }
}