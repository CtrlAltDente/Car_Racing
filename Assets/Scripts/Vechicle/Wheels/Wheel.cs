using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Wheels
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField]
        private WheelCollider _wheelCollider;

        [SerializeField]
        private Transform _wheelModelTransform;

        private void Update()
        {
            SetModelToWheelColliderParameters();
        }

        private void SetModelToWheelColliderParameters()
        {
            _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);

            _wheelModelTransform.position = position;
            _wheelModelTransform.rotation = rotation;
        }
    }
}