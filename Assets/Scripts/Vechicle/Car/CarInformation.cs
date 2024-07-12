using Cars_Racing.Vehicle.Transmission;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public class CarInformation : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Gearbox _gearbox;

        [SerializeField][TextArea]
        private string _carInformation;

        private void Update()
        {
            GetInformation();
        }

        private void GetInformation()
        {
            _carInformation = $"Gear: {_gearbox.CurrentGear}\n" +
                $"Speed: {(_rigidbody.velocity.magnitude * 3.6f).ToString("#.#")}Km/H";
        }
    }
}