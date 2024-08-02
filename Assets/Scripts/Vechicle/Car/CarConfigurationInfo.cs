using Cars_Racing.Vehicle.Configuration.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public class CarConfigurationInfo : MonoBehaviour
    {
        public EngineConfiguration EngineConfiguration;
        public GearboxConfiguration GearboxConfiguration;

        public float AverageGearSpeed => EngineConfiguration.MaxSpeed / GearboxConfiguration.TopGear;
    }
}