using UnityEngine;

namespace PanteonDemo.Circle
{
    public class CircleAnimController : MonoBehaviour
    {
        [SerializeField] private Animator _circleAnim;

        public void CircleAnimActive()
        {
            _circleAnim.SetTrigger("Close");
        }
    }
}