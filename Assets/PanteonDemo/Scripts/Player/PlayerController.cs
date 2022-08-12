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
        [SerializeField] private Camera _mainCam;
        [SerializeField] private float _swipeSpeed, _moveSpeed;
        #endregion

        #region Private Field
        private Rigidbody _boyRigidbody;
        private Vector3 _firstPos, _lastPos;
        private float _newX;
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

        private void Start()
        {
            _boyRigidbody = GetComponent<Rigidbody>();
        }
        #region Update
        private void Update()
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.Failed)
            {
                SetRunAnimation(false);
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Finish)
            {
                MoveToPulpit();
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Play)
            {
                PlayerMovement();
                SetRunAnimation(true);
            }
        }
        #endregion

        #region Void
        private void PlayerMovement()
        {
            _boyRigidbody.velocity = new Vector3(_boyRigidbody.velocity.x, _boyRigidbody.velocity.y, _moveSpeed * Time.fixedDeltaTime);
            if (Input.GetMouseButtonDown(0))
            {
                _firstPos = _mainCam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                _lastPos = _mainCam.ScreenToViewportPoint(Input.mousePosition);
                _newX = Mathf.Clamp(_lastPos.x - _firstPos.x, -0.1f, 0.1f);
                _boyRigidbody.velocity = new Vector3(_newX * _swipeSpeed * Time.fixedDeltaTime, _boyRigidbody.velocity.y, _boyRigidbody.velocity.z);
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

            if (collision.gameObject.CompareTag("Water"))
            {
                GameManager.Instance.SetGameFailed();
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