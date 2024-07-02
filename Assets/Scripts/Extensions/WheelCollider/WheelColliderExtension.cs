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
}
