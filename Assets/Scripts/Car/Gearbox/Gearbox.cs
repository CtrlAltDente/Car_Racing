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

        public int CurrentGear { get => _currentGear; private set => _currentGear = value; }

        public AnimationCurve GearSpeedCurve => new AnimationCurve(new Keyframe(-1, 0.05f), new Keyframe(0, 0.1f), new Keyframe(0.18f, 0.8f), new Keyframe(0.35f, 1), new Keyframe(1, 0.15f), new Keyframe(2, -1));

        public void SetGear(int gear)
        {
            CurrentGear = Mathf.Clamp(gear, -1, _carGearCount);
        }

        public void SetGearChangeValue(bool increaseGear, bool decreaseGear)
        {
            if(increaseGear)
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
            SetGear(CurrentGear + 1);
        }

        private void DecreaseGear()
        {
            SetGear(CurrentGear - 1);
        }
    }
}