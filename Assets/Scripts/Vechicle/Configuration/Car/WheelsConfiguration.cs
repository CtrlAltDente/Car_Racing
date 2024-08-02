using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Configuration.Car
{
    [CreateAssetMenu(fileName = "WheelsConfiguration_", menuName = "Scriptable Objects/Wheels Configuration")]
    public class WheelsConfiguration : ScriptableObject
    {
        public float MinForwardShiftness;
        public float MaxForwardShiftness;
    }
}