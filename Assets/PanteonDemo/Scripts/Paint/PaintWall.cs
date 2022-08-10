using UnityEngine;
using DG.Tweening;

namespace PanteonDemo.Paint
{
    public class PaintWall : MonoBehaviour
    {
        [SerializeField] private PaintIn3D.P3dToggleParticles _particles;
        private void OnEnable()
        {
            PaintWallUp();
        }

        public void PaintWallUp()
        {
            transform.DOLocalMoveY(1f, 1f).OnComplete(() => _particles.enabled = true);
        }
        public void PaintWallDown()
        {
            transform.DOLocalMoveY(-2f, 2f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}