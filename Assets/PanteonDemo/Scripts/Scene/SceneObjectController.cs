using UnityEngine;

namespace PanteonDemo.Scene
{
    public class SceneObjectController : MonoBehaviour
    {
        public static SceneObjectController Instance { get; private set; }

        [SerializeField] private GameObject _paintWall;
        [SerializeField] private GameObject _finalPulpit;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        public void PaintWallActive()
        {
            _paintWall.SetActive(true);
        }

        public void FinalPulpitActive()
        {
            _finalPulpit.SetActive(true);
        }
    }
}