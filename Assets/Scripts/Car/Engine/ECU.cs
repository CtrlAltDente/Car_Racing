using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECU : MonoBehaviour
{
    [SerializeField]
    private float _maxRPM;

    public float RPMEfficientValue => CurrentRPM / _maxRPM;
    public float CurrentRPM { get; private set; }

    private float _rpmImpulse;
    private float _rpmImpulseIncreasing;

    private void Update()
    {

    }

    public void CalculateRPM(float gas)
    {
        CurrentRPM = Mathf.Clamp(CurrentRPM + _maxRPM * gas * Cars_Racing.Vehicle.Car.CarInformation.CarConfiguration.HorsePower * Time.deltaTime, 0, _maxRPM); 
    }

    private void CalculateImpulse()
    {
        
    }
}