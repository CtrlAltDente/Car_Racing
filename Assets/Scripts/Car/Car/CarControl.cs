using Cars_Racing.InputLogics;
using Cars_Racing.Vehicle.EngineLogic;
using Cars_Racing.Vehicle.SteeringWheelLogic;
using Cars_Racing.Vehicle.Transmission;
using Cars_Racing.Vehicle.Wheels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public class CarControl : MonoBehaviour
    {
        [SerializeField]
        private SteeringWheel _steeringWheel;
        [SerializeField]
        private Gearbox _gearbox;

        public void SetupInputToComponets(CarInputData carInputData)
        {
            _steeringWheel.SetSteeringWheelValue(carInputData.SteeringWheelInput);
            _gearbox.SetGearChangeValue(carInputData.GearIncreaseInput, carInputData.GearDecreaseInput);
        }
    }
}