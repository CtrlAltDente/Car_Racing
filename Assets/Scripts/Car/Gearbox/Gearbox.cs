using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Cars_Racing.Vehicle.Transmission
{
    public class Gearbox : MonoBehaviour
    {
        [SerializeField]
        private int _currentGear = 0;

        [SerializeField]
        private int _carGearCount;

        private Coroutine _switchGearCoroutine;

        public int CurrentGear { get => _currentGear; private set => _currentGear = value; }
        public bool IsGearSwitching { get; private set; }
        public bool IsNeutralGear => _currentGear == 0;

        public IEnumerator SetGear(int gear)
        {
            IsGearSwitching = true;
            CurrentGear = Mathf.Clamp(gear, -1, _carGearCount);
            yield return new WaitForSeconds(0.5f);
            IsGearSwitching = false;
        }

        public void SetGearChangeValue(bool increaseGear, bool decreaseGear)
        {
            if (increaseGear)
            {
                IncreaseGear();
            }
            else if (decreaseGear)
            {
                DecreaseGear();
            }
        }

        private void IncreaseGear()
        {
            if (CurrentGear == _carGearCount)
                return;

            _switchGearCoroutine = StartCoroutine(SetGear(CurrentGear + 1));
        }

        private void DecreaseGear()
        {
            if (CurrentGear == -1)
                return;

            _switchGearCoroutine = StartCoroutine(SetGear(CurrentGear - 1));
        }
    }
}