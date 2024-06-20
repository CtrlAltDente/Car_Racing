using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasWheels : MonoBehaviour
{
    [SerializeField]
    private WheelCollider[] _wheels;

    private float _motorTorgue = 0;

    private void Update()
    {
        if (_motorTorgue > 0)
        {
            _motorTorgue -= 500 * Time.deltaTime;
        }
        else if (_motorTorgue < 0)
            _motorTorgue = 0;

        foreach (var wheel in _wheels)
        {
            wheel.motorTorque = _motorTorgue;
        }
    }

    public void Gas()
    {
        _motorTorgue = 1000;
    }
}
