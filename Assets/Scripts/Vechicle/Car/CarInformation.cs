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

        [SerializeField]
        private WheelCollider[] _wheelColliders;

        [SerializeField]
        [TextArea(10,100)]
        private string _carInformation;

        private void Update()
        {
            GetInformation();
        }

        private void GetInformation()
        {
            _carInformation = GetCurrentGear() + GetSpeed() + GetMotorTorque();
            ;
        }

        private string GetCurrentGear()
        {
            return $"Gear: {_gearbox.CurrentGear}\n";
        }

        private string GetSpeed()
        {
            return $"Speed: {(_rigidbody.velocity.magnitude * 3.6f).ToString("#.#")}Km/H\n";
        }

        private string GetMotorTorque()
        {
            string wheelsMotorTorque = string.Empty;
            for (int i = 1; i <= _wheelColliders.Length; i++)
            {
                wheelsMotorTorque += $"Wheel {1} motor torque: {_wheelColliders[i-1].motorTorque}\n";
            }

            return wheelsMotorTorque;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + _rigidbody.centerOfMass, 0.1f);
        }
    }
}