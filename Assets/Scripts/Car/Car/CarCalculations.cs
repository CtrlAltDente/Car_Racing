using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public static class CarCalculations
    {
        public static AnimationCurve MotorTorgueGearCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0.2f));
        public static AnimationCurve MotorTorqueCurve = new AnimationCurve(new Keyframe(-0.4f, -1f), new Keyframe(0, 0.4f), new Keyframe(0.5f, 1), new Keyframe(0.75f, 0.6f), new Keyframe(1, 0f), new Keyframe(1.4f, -1f));

        public static AnimationCurve RPMPowerToGearSpeedCurve = new AnimationCurve(new Keyframe(-0.5f, -1f), new Keyframe(0, 0.1f), new Keyframe(0.7f, 1), new Keyframe(1, 0));
        public static AnimationCurve RPMPower = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

        public static float CalculateRPM(float gasValue, float currentRPM, float maxRPM, float currentGear, float topGear, float currentSpeed, float maxSpeed, float horsePower)
        {
            float minRPM = 1000;
            float avgGearSpeed = maxSpeed / topGear;
            float currentGearMinSpeed = (avgGearSpeed * (currentGear - 1) * 1.2f);
            float currentGearMaxSpeed = (avgGearSpeed * currentGear * 1.2f);
            float currentGearRPMPowerBySpeed = Mathf.Lerp(-0.5f, 1f, (currentSpeed - currentGearMinSpeed) / (currentGearMaxSpeed - currentGearMinSpeed));
            float rpmPower = RPMPowerToGearSpeedCurve.Evaluate(currentGearRPMPowerBySpeed);

            Debug.Log($"Rpm power: { rpmPower.ToString("#.#")}");

            if (gasValue != 0)
            {
                currentRPM = Mathf.Clamp(currentRPM + rpmPower * (horsePower * 10 * Time.deltaTime), minRPM, maxRPM);

            }
            else
            {
                currentRPM = Mathf.Clamp(currentRPM - horsePower * 10 * Time.deltaTime, minRPM, maxRPM);
            }

            return currentRPM;
        }

        public static float CalculateMotorTorque(float currentRPM, float maxRPM, float currentGear, float topGear, float currentSpeed, float maxSpeed, float horsePower)
        {
            float avgGearSpeed = maxSpeed / topGear;
            float currentGearMinSpeed = (avgGearSpeed * (currentGear - 1)) - (10);
            float currentGearMaxSpeed = (avgGearSpeed * currentGear) + (10);
            float currentGearSpeed = Mathf.Lerp(-0.2f, 1.2f, (Mathf.Abs(currentSpeed) - currentGearMinSpeed) / (currentGearMaxSpeed - currentGearMinSpeed));
            float increasingSpeedValue = MotorTorqueCurve.Evaluate(currentGearSpeed);

            float gearMultiplier = MotorTorgueGearCurve.Evaluate(Mathf.Abs(currentGear) / topGear);

            float torque = (horsePower * 745.7f) / ((2 * Mathf.PI * currentRPM) / 60);

            float rpmPower = RPMPower.Evaluate(currentRPM / maxRPM);

            //Debug.Log($"Current gear speed multiplier: {currentGearSpeed}, speed value: {increasingSpeedValue}");

            return increasingSpeedValue * gearMultiplier * torque * rpmPower;
        }
    }
}