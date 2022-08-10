using UnityEngine;
using DG.Tweening;

namespace PanteonDemo.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Singleton
        public static PlayerController Instance { get; private set; }
        #endregion

        #region Private Serialize Field
        [Header("Animation")]
        [SerializeField] private Animator _boyAnimator;

        [Header("Arrow")]
        [SerializeField] private GameObject _arrow;

        [Header("Move Referance")]
        [SerializeField] private float _minX, _maxX;
        [SerializeField] private float _playerMoveSpeed;
        #endregion

        #region Private Field
        // Move Referance
        private Vector3 _firsPos, _endPos, newPos;
        private float _posX;
        #endregion

        #region Awake
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        #endregion

        #region Update
        private void Update()
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.Play)
            {
                PlayerMovement();
                SetRunAnimation(true);
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Failed)
            {
                SetRunAnimation(false);
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Finish)
            {
                MoveToPulpit();
            }

        }
        #endregion

        #region Void
        private void PlayerMovement()
        {
            transform.position += Vector3.forward * _playerMoveSpeed * Time.deltaTime;

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
        }

        private void SetAnimations(string triggerName)
        {
            _boyAnimator.SetTrigger(triggerName);
        }
        private void SetRunAnimation(bool value)
        {
            _boyAnimator.SetBool("Run", value);
        }

        private void MoveToPulpit()
        {
            transform.DOLocalMove(new Vector3(0, 0.3f, 15f), 1f);
            transform.DOLocalRotate(new Vector3(0, 180, 0), 1f).OnComplete(() =>
            {
                SetAnimations("Victory");
                UI.UIManager.Instance.SetPanelVisible(1);
            });
        }
        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            IDamaging damaging = collision.gameObject.GetComponent<IDamaging>();
            IPainting painting = collision.gameObject.GetComponent<IPainting>();

            if (damaging != null)
            {
                // Burada hasar yiyince oyun failed oluyor
                damaging.Damage();
                // Dead animasyonu çalýþýyor
                SetAnimations("Dead");
            }

            if (painting != null)
            {
                // Burada paint moduna geçiyor
                painting.GamePaint();
                // koþma animasyonu duruyor
                SetRunAnimation(false);
            }

            if (collision.gameObject.CompareTag("Rotator"))
            {
                GameManager.Instance.SetGameStat(GameManager.GameStat.Stop);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Rotator"))
            {
                GameManager.Instance.SetGameStat(GameManager.GameStat.Play);
            }
        }
    }
}