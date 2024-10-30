using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Transmission
{
    public class Transmission : MonoBehaviour
    {
        [SerializeField]
        private CarConfiguration _carConfiguration;

        [SerializeField]
        private Gearbox _gearbox;

        public Gearbox Gearbox => _gearbox;

        public float CurrentGearMinSpeed => CarConstants.CalculateGearAndRpmSpeed(_carConfiguration.GearboxConfiguration.Gears[_gearbox.GearIndex], _carConfiguration.EngineConfiguration.MinRPM) * 3.6f;
        public float CurrentGearMaxSpeed => CarConstants.CalculateGearAndRpmSpeed(_carConfiguration.GearboxConfiguration.Gears[_gearbox.GearIndex], _carConfiguration.EngineConfiguration.MaxRPM) * 3.6f;
    }
}