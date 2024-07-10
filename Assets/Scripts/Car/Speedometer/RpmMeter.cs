using Cars_Racing.Vehicle.Car;
using Cars_Racing.Vehicle.EngineLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.SpeedometerLogic
{
    public class RpmMeter : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _pointer;

        [SerializeField]
        private Engine _engine;

        private void Update()
        {
            ShowRpm();
        }

        private void ShowRpm()
        {
            float rpmEfficient = _engine.CurrentRPM / CarConfigurationInfo.CarConfiguration.MaxRPM;

            _pointer.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 140), Quaternion.Euler(0, 0, -30), rpmEfficient);
        }
    }
}