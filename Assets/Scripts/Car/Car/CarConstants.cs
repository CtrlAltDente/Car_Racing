using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public static class CarConstants
    {
        public static AnimationCurve MotorTorgueGearCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0.2f));
        public static AnimationCurve CarSpeedCurve = new AnimationCurve(new Keyframe(-0.2f, 0f), new Keyframe(0, 0.4f), new Keyframe(0.5f, 0.8f), new Keyframe(0.75f, 0.6f), new Keyframe(1, 0.5f), new Keyframe(1.2f, 0f));

        public static float CalculateMotorTorque(float currentRPM, float maxRPM, float currentGear, float topGear, float currentSpeed, float maxSpeed, float horsePower)
        {
            float avgGearSpeed = maxSpeed / topGear;

            float currentGearMinSpeed = (avgGearSpeed * (currentGear - 1)) - (avgGearSpeed * 0.2f);
            float currentGearMaxSpeed = (avgGearSpeed * currentGear) + (avgGearSpeed * 0.2f);
            float currentGearSpeed = Mathf.Lerp(-0.2f, 1.2f, (currentSpeed - currentGearMinSpeed) / (currentGearMaxSpeed - currentGearMinSpeed));

            float rpmEfficient = currentRPM / maxRPM;

            float maxTorque = horsePower * 5252 / maxRPM;
            float gearMultiplier = MotorTorgueGearCurve.Evaluate(Mathf.Abs(currentGear) / topGear);
            float increasingSpeedValue = CarSpeedCurve.Evaluate(currentGearSpeed);

            Debug.Log($"Current gear speed multiplier: {currentGearSpeed}, speed value: {increasingSpeedValue}");

            return rpmEfficient * maxTorque * gearMultiplier * increasingSpeedValue;
        }
    }
}