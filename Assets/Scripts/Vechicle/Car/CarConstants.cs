using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public static class CarConstants
    {
        public static AnimationCurve MotorTorqueGearCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0.6f));
        
        public static AnimationCurve PowerToGearSpeedCurve = new AnimationCurve(new Keyframe(-0.2f, 0), new Keyframe(0, 0.3f), new Keyframe(0.2f, 1), new Keyframe(0.8f, 1), new Keyframe(1, 0));
        public static AnimationCurve RPMPower = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

        public static float CalculateGearAndRpmSpeed(Gear gear, float rpm)
        {
            return (rpm * Mathf.PI * 0.32f) / (gear.GearRatio * gear.DifferentialRatio * 60);
        }

        public static float CalculateMotorTorque(int horsePower, float rpm)
        {
            return (horsePower * 745.7f) / ((2 * Mathf.PI * rpm) / 60);
        }
    }
}