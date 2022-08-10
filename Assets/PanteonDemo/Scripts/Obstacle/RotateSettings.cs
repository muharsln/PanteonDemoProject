using UnityEngine;

namespace PanteonDemo.Obstacle
{
    [CreateAssetMenu(menuName = "PanteonDemo/ScriptableObject/RotateSettings")]
    public class RotateSettings : ScriptableObject
    {
        [SerializeField] private Vector3 _rotateAngle;
        public Vector3 rotateAngle { get { return _rotateAngle; } set { _rotateAngle = value; } }

        [SerializeField] private float _rotateSpeed;
        public float rotateSpeed { get { return _rotateSpeed; } set { _rotateSpeed = value; } }
    }
}