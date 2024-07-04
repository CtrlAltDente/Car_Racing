using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECU : MonoBehaviour
{
    [SerializeField]
    private float _maxRPM;

    [SerializeField]
    private string _information;

    public float RPMEfficientValue => CurrentRPM / _maxRPM;
    public float MaxRPM => _maxRPM;
    public float CurrentRPM { get; private set; }

    private float _rpmIncreasing => _maxRPM / 3;

    private void Update()
    {
        GetInformation();
    }

    public void CalculateRPM(float gas)
    {
        CurrentRPM = Mathf.Clamp(Mathf.MoveTowards(CurrentRPM, gas * _maxRPM, Mathf.Abs(gas) * Time.deltaTime * _rpmIncreasing), 0, _maxRPM);
    }

    private void GetInformation()
    {
        _information = CurrentRPM.ToString("#.#");
    }
}