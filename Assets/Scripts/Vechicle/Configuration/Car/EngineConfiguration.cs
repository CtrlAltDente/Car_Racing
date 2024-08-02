using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Configuration.Car
{
    [CreateAssetMenu(fileName = "EngineConfiguration_", menuName = "Scriptable Objects/Engine Configuration")]
    public class EngineConfiguration : ScriptableObject
    {
        public float MinRPM;
        public float MaxRPM;
        public int MaxSpeed;
        public int HorsePower;
    }
}