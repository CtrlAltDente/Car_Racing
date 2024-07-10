using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    [Serializable]
    public struct CarConfiguration
    {
        public float MinRPM;
        public float MaxRPM;
        public int MaxSpeed;
        public int TopGear;
        public int HorsePower;
    }
}