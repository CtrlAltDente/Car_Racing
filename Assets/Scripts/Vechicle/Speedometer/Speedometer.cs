using Cars_Racing.Vehicle.Car;
using Cars_Racing.Vehicle.EngineLogic;
using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cars_Racing.Vehicle.SpeedometerLogic
{
    public class Speedometer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _pointer;

        [SerializeField]
        private TextMeshProUGUI _speedText;
        [SerializeField]
        private TextMeshProUGUI _geartext;

        [SerializeField]
        private Engine _engine;
        [SerializeField]
        private Gearbox _gearbox;

        private void Update()
        {
            ShowRpm();
            ShowSpeed();
            ShowGear();
        }

        private void ShowRpm()
        {
            float rpmEfficient = _engine.CurrentRPM / CarConfigurationInfo.CarConfiguration.MaxRPM;

            _pointer.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 140), Quaternion.Euler(0, 0, -30), rpmEfficient);
        }

        private void ShowSpeed()
        {
            _speedText.text = (Mathf.Abs((int)_engine.CurrentSpeed)).ToString("#####");
        }

        private void ShowGear()
        {
            string gearText = _gearbox.CurrentGear < 0 ? "R" : _gearbox.CurrentGear == 0 ? "N" : _gearbox.CurrentGear.ToString("##");
            _geartext.text = gearText;
        }
    }
}