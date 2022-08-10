using UnityEngine;

namespace PanteonDemo.Ground
{
    public class FinishGround : MonoBehaviour, IPainting
    {
        public void GamePaint()
        {
            GameManager.Instance.SetGameStat(GameManager.GameStat.Paint);
            Scene.SceneObjectController.Instance.PaintWallActive();
        }
    }
}