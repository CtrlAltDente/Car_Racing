using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cars_Racing.InputLogics
{
    public class CarInputActions : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _gasAction;
        [SerializeField]
        private InputActionReference _footBreak;
        [SerializeField]
        private InputActionReference _steeringWheelAction;
        [SerializeField]
        private InputActionReference _handBreakAction;
        [SerializeField]
        private InputActionReference _gearIncreaseAction;
        [SerializeField]
        private InputActionReference _gearDecreaseAction;

        public float GasInput => _gasAction.action.ReadValue<float>();
        public float FootBreak => _footBreak.action.ReadValue<float>();
        public float SteeringWheelAction => _steeringWheelAction.action.ReadValue<float>();
        public float HandBreakInput => _handBreakAction.action.ReadValue<float>();
        public bool GearIncreaseInput => _gearIncreaseAction.action.triggered;
        public bool GearDecreaseInput => _gearDecreaseAction.action.triggered;

        public CarInputData PlayerInputData => new CarInputData(GasInput, FootBreak, SteeringWheelAction, HandBreakInput, GearIncreaseInput, GearDecreaseInput);
    }
}