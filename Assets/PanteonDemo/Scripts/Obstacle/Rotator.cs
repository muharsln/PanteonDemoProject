using UnityEngine;

namespace PanteonDemo.Obstacle
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private RotateSettings _rotateSettings;

        private Rigidbody _rotatorRigidbody;

        void Start()
        {
            _rotatorRigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            Quaternion deltaRotation = Quaternion.Euler(_rotateSettings.rotateAngle * _rotateSettings.rotateSpeed * Time.fixedDeltaTime);
            _rotatorRigidbody.MoveRotation(_rotatorRigidbody.rotation * deltaRotation);
        }
    }
}