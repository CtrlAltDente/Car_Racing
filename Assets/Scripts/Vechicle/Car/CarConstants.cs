using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public static class CarConstants
    {
        public static AnimationCurve MotorTorgueGearCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0.2f));
        
        public static AnimationCurve RPMPowerToGearSpeedCurve = new AnimationCurve(new Keyframe(0, 0f), new Keyframe(0.7f, 1), new Keyframe(1, 0));
        public static AnimationCurve RPMPower = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
    }
}