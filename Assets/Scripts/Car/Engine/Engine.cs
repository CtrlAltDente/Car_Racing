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

        [SerializeField]
        private string _information;

        public void SetEngineValues(float gasValue, float footBreakValue)
        {
            Gas(gasValue);
            FootBreak(footBreakValue);
        }

        private void Gas(float gasValue)
        {
            float motorTorque = 0;

            if (!_gearbox.IsGearSwitching && !_gearbox.IsNeutralGear && gasValue > 0)
            {
                _ecu.CalculateRPM(gasValue);
                motorTorque = gasValue * CarConstants.CalculateMotorTorque(_ecu.CurrentRPM, _ecu.MaxRPM, _gearbox.CurrentGear, _gearbox.TopGear, _wheelColliders.GetSpeed(), 200, CarConfigurationInfo.CarConfiguration.HorsePower);
            }
            else
            {
                _ecu.CalculateRPM(-1f);
                motorTorque = 0;
            }

            _information = $"Motor torque: {motorTorque.ToString("#.#")}, Speed: {_wheelColliders.GetSpeed()}";

            _wheelColliders.DoWheelAction(wheel => wheel.motorTorque = motorTorque);
        }

        private void FootBreak(float breakValue)
        {

        }
    }
}