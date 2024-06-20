using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeWheels : MonoBehaviour
{
    [SerializeField]
    private WheelCollider[] _wheels;

    private float _brakeTorgue = 0;

    private void Update()
    {
        if(_brakeTorgue > 0)
        {
            _brakeTorgue -= 500 * Time.deltaTime;
        }
        else if (_brakeTorgue < 0)
            _brakeTorgue = 0;

        foreach (var wheel in _wheels)
        {
            wheel.brakeTorque = _brakeTorgue;
        }
    }

    public void Break()
    {
        _brakeTorgue = 1000;
    }
}
