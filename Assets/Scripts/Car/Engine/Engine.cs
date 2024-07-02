using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.EngineLogic
{
    public class Engine : MonoBehaviour
    {
        [SerializeField]
        private Gearbox _gearbox;
        [SerializeField]
        private ECU _ecu;

        [SerializeField]
        private WheelCollider[] _wheelColliders;

        public void SetEngineValues(float gasValue, float footBreakValue)
        {
            Gas(gasValue);
            FootBreak(footBreakValue);
        }

        private void Gas(float gasValue)
        {
            if (!_gearbox.IsGearSwitching)
            {
                _ecu.CalculateRPM(gasValue);

                foreach (var wheelCollider in _wheelColliders)
                {
                    wheelCollider.motorTorque = gasValue * _ecu.GearEfficientValue * _gearbox.CurrentGear * 1000f;
                }
            }
            else
            {
                _ecu.CalculateRPM(0);

                foreach (var wheelCollider in _wheelColliders)
                {
                    wheelCollider.motorTorque = 0 * _ecu.GearEfficientValue * _gearbox.CurrentGear * 1000f;
                }
            }

            Debug.Log($"RPM: {_ecu.CurrentRPM}");
        }

        private void FootBreak(float breakValue)
        {

        }
    }
}