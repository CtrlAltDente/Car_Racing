using Cars_Racing.Vehicle.Car;
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
            float motorTorque;

            if (!_gearbox.IsGearSwitching && !_gearbox.IsNeutralGear)
            {
                _ecu.CalculateRPM(gasValue);
                motorTorque = gasValue * _ecu.RPMEfficientValue * _gearbox.CurrentGear * CarInformation.CarConfiguration.HorsePower;
            }
            else
            {
                _ecu.CalculateRPM(-0.2f);
                motorTorque = _ecu.RPMEfficientValue * CarInformation.CarConfiguration.HorsePower;
            }

            _wheelColliders.DoWheelAction(wheel => wheel.motorTorque = motorTorque);
        }

        private void FootBreak(float breakValue)
        {

        }
    }
}