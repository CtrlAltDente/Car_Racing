using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Cars_Racing.Vehicle.Transmission
{
    public class Gearbox : MonoBehaviour
    {
        public CarConfigurationInfo CarConfigurationInfo;

        [SerializeField]
        private int _currentGear = 0;

        private Coroutine _switchGearCoroutine;

        public int CurrentGear { get => _currentGear; private set => _currentGear = value; }
        public int TopGear { get => CarConfigurationInfo.GearboxConfiguration.TopGear; }
        public bool IsGearSwitching { get; private set; }
        public bool IsNeutralGear => _currentGear == 0;

        public IEnumerator SetGear(int gear, bool pause)
        {
            IsGearSwitching = true;
            CurrentGear = Mathf.Clamp(gear, -1, TopGear);
            
            if (pause)
            {
                yield return new WaitForSeconds(0.5f);
            }
            
            IsGearSwitching = false;
            yield return null;
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
            if (CurrentGear == TopGear)
                return;

            _switchGearCoroutine = StartCoroutine(SetGear(CurrentGear + 1, CurrentGear != 0));
        }

        private void DecreaseGear()
        {
            if (CurrentGear == -1)
                return;

            _switchGearCoroutine = StartCoroutine(SetGear(CurrentGear - 1, CurrentGear != 0));
        }
    }
}