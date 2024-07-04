using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WheelColliderExtension
{
    public static void DoWheelAction(this List<WheelCollider> wheelColliders, Action<WheelCollider> wheelAction)
    {
        foreach (var wheelCollider in wheelColliders)
        {
            wheelAction?.Invoke(wheelCollider);
        }
    }

    public static void DoWheelAction(this WheelCollider[] wheelColliders, Action<WheelCollider> wheelAction)
    {
        foreach (var wheelCollider in wheelColliders)
        {
            wheelAction?.Invoke(wheelCollider);
        }
    }

    public static float GetSpeed(this WheelCollider[] wheelColliders)
    {
        float avgSpeed = 0;

        foreach (var wheelCollider in wheelColliders)
        {
            avgSpeed += 2 * Mathf.PI * wheelCollider.radius * wheelCollider.rpm * 60 / 1000;
        }

        return avgSpeed / wheelColliders.Length;
    }

    public static float GetSpeed(this List<WheelCollider> wheelColliders)
    {
        float avgSpeed = 0;

        foreach (var wheelCollider in wheelColliders)
        {
            avgSpeed += 2 * Mathf.PI * wheelCollider.radius * wheelCollider.rpm * 60;
        }

        return avgSpeed / wheelColliders.Count;
    }
}
