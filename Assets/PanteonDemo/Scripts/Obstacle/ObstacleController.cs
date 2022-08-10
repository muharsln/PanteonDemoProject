using UnityEngine;

namespace PanteonDemo.Obstacle
{
    public class ObstacleController : MonoBehaviour, IDamaging
    {
        public void Damage()
        {
            GameManager.Instance.SetGameFailed();
        }
    }
}