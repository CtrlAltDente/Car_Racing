using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECU : MonoBehaviour
{
    [SerializeField]
    private float _maxRPM;

    [SerializeField]
    private float _ecuLevel;

    public float GearEfficientValue => CurrentRPM / _maxRPM;
    public float CurrentRPM { get; private set; }

    public void CalculateRPM(float gas)
    {
        CurrentRPM = Mathf.Lerp(CurrentRPM, _maxRPM * gas, (1 + gas * 1) * Time.deltaTime);
    }
}
