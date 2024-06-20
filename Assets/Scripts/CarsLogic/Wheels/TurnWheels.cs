using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWheels : MonoBehaviour
{
    [SerializeField]
    public WheelCollider[] _wheels;

    public void Turn(float direction)
    {
        foreach (var wheel in _wheels)
        {
            wheel.steerAngle = direction;
        }
    }
}
