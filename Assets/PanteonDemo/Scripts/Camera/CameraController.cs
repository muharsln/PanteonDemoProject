using UnityEngine;
using DG.Tweening;

namespace PanteonDemo.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Follow")]
        [SerializeField] private Transform _target;
        [SerializeField] private float _lerpSpeed;

        private void LateUpdate()
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.Play)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition,
                    new Vector3(_target.localPosition.x, transform.localPosition.y,
                    _target.localPosition.z - 0.9f), _lerpSpeed * Time.deltaTime);
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Paint)
            {
                transform.DOLocalMove(new Vector3(0, 1, 13), 1f);
                transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Finish)
            {
                transform.DOLocalMoveZ(12f, 1f);
            }
        }
    }
}