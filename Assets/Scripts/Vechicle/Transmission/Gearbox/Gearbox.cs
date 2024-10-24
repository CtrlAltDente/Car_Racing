using Cars_Racing.Vehicle.Car;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Cars_Racing.Vehicle.Transmission
{
    public class Gearbox : MonoBehaviour
    {
        public CarConfiguration CarConfiguration;

        [SerializeField]
        private int _gearIndex = 1;

        private Coroutine _switchGearCoroutine;

        public bool IsNeutralGear => CarConfiguration.GearboxConfiguration.Gears[_gearIndex].GearNumber == 0;
        public bool IsNegativeGear => CarConfiguration.GearboxConfiguration.Gears[_gearIndex].GearNumber < 0;

        public int GearIndex => _gearIndex;
        public int GearNumber => CarConfiguration.GearboxConfiguration.Gears[_gearIndex].GearNumber;
        
        public bool IsGearSwitching { get; private set; }

        public IEnumerator SetGear(int gear, bool pause)
        {
            IsGearSwitching = true;
            _gearIndex = Mathf.Clamp(gear, 0, CarConfiguration.GearboxConfiguration.Gears.Length-1);
            
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
            if (_gearIndex == CarConfiguration.GearboxConfiguration.Gears.Length - 1)
                return;

            _switchGearCoroutine = StartCoroutine(SetGear(_gearIndex + 1, _gearIndex != 0));
        }

        private void DecreaseGear()
        {
            if (_gearIndex == 0)
                return;

            _switchGearCoroutine = StartCoroutine(SetGear(_gearIndex - 1, _gearIndex != 0));
        }

    }
}