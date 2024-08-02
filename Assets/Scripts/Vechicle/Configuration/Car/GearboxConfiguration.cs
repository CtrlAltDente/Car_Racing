using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Configuration.Car
{
    [CreateAssetMenu(fileName = "GearboxConfiguration_", menuName = "Scriptable Objects/Gearbox Configuration")]
    public class GearboxConfiguration : ScriptableObject
    {
        public int TopGear;
    }
}