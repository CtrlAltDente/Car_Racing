using Cars_Racing.Vehicle.Car;
using Cars_Racing.Vehicle.EngineLogic;
using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cars_Racing.Vehicle.SpeedometerLogic
{
    public class Speedometer : MonoBehaviour
    {
        public CarConfigurationInfo CarConfigurationInfo;

        [SerializeField]
        private Image _rpmImageValue;
        
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
            float rpmEfficient = _engine.CurrentRPM / CarConfigurationInfo.EngineConfiguration.MaxRPM;
            float rpmValue = Mathf.Lerp(0, 0.83333f, rpmEfficient);
            _rpmImageValue.fillAmount = rpmValue;
        }

        private void ShowSpeed()
        {
            int speed = Mathf.Abs(_engine.CurrentSpeed) < 0.5f ? 0 : (int)Mathf.Abs(_engine.CurrentSpeed);
            string speedText = speed.ToString("#####");
            _speedText.text = speedText == string.Empty ? 0.ToString() : speedText;

            Debug.Log($"Speed: {speed}");
        }

        private void ShowGear()
        {
            string gearText = _gearbox.CurrentGear < 0 ? "R" : _gearbox.CurrentGear == 0 ? "N" : _gearbox.CurrentGear.ToString("##");
            _geartext.text = gearText;
        }
    }
}