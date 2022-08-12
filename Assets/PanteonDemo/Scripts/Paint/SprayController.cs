using UnityEngine;
using DG.Tweening;
using TMPro;

namespace PanteonDemo.Paint
{
    public class SprayController : MonoBehaviour
    {
        [Header("Move Referance")]
        [SerializeField] private float _minX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxX;
        [SerializeField] private float _maxY;
        [SerializeField] private float _playerMoveSpeed;

        [Header("Text")]
        [SerializeField] private TextMeshPro _valueText;

        [Header("Paint")]
        [SerializeField] private PaintWall _paintWall;

        // Move Referance
        private Vector3 _firsPos, _endPos, newPos;
        private float _posX, _posY;

        // Text
        private int _valueCount;
        private float[] _updatedPos = new float[] { -2f, .5f, 3f };


        private void Update()
        {
            // Left and right movement
            if (Input.GetMouseButtonDown(0))
            {
                _firsPos = Input.mousePosition;
                _posX = transform.localPosition.x;
                _posY = transform.localPosition.y;
            }
            if (Input.GetMouseButton(0))
            {
                _endPos = Input.mousePosition;
                newPos.x = ((_endPos.x - _firsPos.x) / (Screen.width / 4 * _playerMoveSpeed)) + _posX;
                newPos.x = Mathf.Clamp(newPos.x, _minX, _maxX);
                newPos.y = ((_endPos.y - _firsPos.y) / (Screen.height / 4 * _playerMoveSpeed)) + _posY;
                newPos.y = Mathf.Clamp(newPos.y, _minY, _maxY);
                transform.localPosition = new Vector3(newPos.x, newPos.y, transform.localPosition.z);
            }

            _valueCount = int.Parse(_valueText.text);

            if (_valueCount == 100)
            {
                _paintWall.PaintWallDown();
                // Eðer oyuncunun sýrasý 1 ise
                if (Rank.RankController.Instance.GetBoyRank() == 1)
                {
                    GameManager.Instance.SetGameStat(GameManager.GameStat.Finish);
                    Scene.SceneObjectController.Instance.FinalPulpitActive();
                }

                else
                {
                    GameManager.Instance.SetGameFailed();
                }

            }
        }
    }
}