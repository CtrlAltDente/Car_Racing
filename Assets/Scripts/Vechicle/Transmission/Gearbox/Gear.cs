using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Cars_Racing.Vehicle.Transmission
{
    [Serializable]
    public struct Gear
    {
        public int GearNumber;
        public float GearRatio;
        public float DifferentialRatio;
    }
}