using UnityEngine;
using DG.Tweening;
using TMPro;

namespace PanteonDemo.Paint
{
    public class SprayController : MonoBehaviour
    {
        [Header("Move Referance")]
        [SerializeField] private float _minX, _maxX;
        [SerializeField] private float _playerMoveSpeed;

        [Header("Text")]
        [SerializeField] private TextMeshPro _valueText;

        [Header("Paint")]
        [SerializeField] private PaintWall _paintWall;

        // Move Referance
        private Vector3 _firsPos, _endPos, newPos;
        private float _posX;

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
            }
            if (Input.GetMouseButton(0))
            {
                _endPos = Input.mousePosition;
                newPos.x = ((_endPos.x - _firsPos.x) / (Screen.width / 4 * _playerMoveSpeed)) + _posX;
                newPos.x = Mathf.Clamp(newPos.x, _minX, _maxX);
                transform.localPosition = new Vector3(newPos.x, transform.localPosition.y, transform.localPosition.z);
            }

            _valueCount = int.Parse(_valueText.text);

            switch (_valueCount)
            {
                case 25:
                    SprayUp(0);
                    break;
                case 50:
                    SprayUp(1);
                    break;
                case 75:
                    SprayUp(2);
                    break;
                case 100:
                    GameManager.Instance.SetGameStat(GameManager.GameStat.Finish);
                    _paintWall.PaintWallDown();
                    Scene.SceneObjectController.Instance.FinalPulpitActive();
                    break;
                default:
                    break;
            }
        }

        private void SprayUp(int element)
        {
            transform.DOLocalMoveY(_updatedPos[element], 1f);
        }
    }
}