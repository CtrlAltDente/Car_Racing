using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    [CreateAssetMenu(fileName = "Car_Constants", menuName = "Scriptable Objects/Car Constants", order = 0)]
    public class CarConstants : ScriptableObject
    {
        public AnimationCurve CarSpeedCurve = new AnimationCurve(new Keyframe(-1, 0.2f), new Keyframe(0, 0.4f), new Keyframe(0.5f, 0.6f), new Keyframe(1, 0.8f), new Keyframe(1.5f, 0.6f), new Keyframe(2, 0.4f));
    }
}