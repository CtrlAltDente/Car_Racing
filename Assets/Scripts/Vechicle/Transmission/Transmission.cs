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

        public float CurrentGearMinSpeed => (_carConfiguration.EngineConfiguration.MinRPM * Mathf.PI * 0.32f) / (_carConfiguration.GearboxConfiguration.Gears[_gearbox.GearIndex].GearRatio * 4.1f * 60) * 3.6f;
        public float CurrentGearMaxSpeed => (_carConfiguration.EngineConfiguration.MaxRPM * Mathf.PI * 0.32f) / (_carConfiguration.GearboxConfiguration.Gears[_gearbox.GearIndex].GearRatio * 4.1f * 60) * 3.6f;
    }
}