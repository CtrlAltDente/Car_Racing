using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cars_Racing.InputLogics
{
    public class CarInput : MonoBehaviour
    {
        public UnityEvent<CarInputData> OnCarInputDataReady;

        [SerializeField]
        private CarInputActions _carInputActions;

        [SerializeField]
        private CarInputData _carInputData;

        public void Update()
        {
            _carInputData = _carInputActions.PlayerInputData;
            OnCarInputDataReady?.Invoke(_carInputData);
        }
    }
}