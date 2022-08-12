using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
namespace PanteonDemo.AI
{
    public class AgentController : MonoBehaviour
    {
        [SerializeField] private Animator _agentAnim;
        [SerializeField] private Transform _target;

        private NavMeshAgent _agent;
        private float _randomXPos;
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
            _agent = GetComponent<NavMeshAgent>();
            _randomXPos = Random.Range(-1.2f, 1.2f);
        }
        private void Update()
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.Play)
            {
                AgentMove();
            }

            if (GameManager.Instance.gameStat == GameManager.GameStat.Paint || GameManager.Instance.gameStat == GameManager.GameStat.Failed)
            {
                AgentSetRunAnim(false);
                _agent.isStopped = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            IPainting painting = collision.gameObject.GetComponent<IPainting>();
            IDamaging damaging = collision.gameObject.GetComponent<IDamaging>();

            if (painting != null && GameManager.Instance.gameStat != GameManager.GameStat.Paint)
            {
                AgentSetAnim("Victory");
                transform.DOLocalMoveZ(transform.localPosition.z + 1, 0.5f);
                transform.DOLocalRotate(Vector3.up * 180, 0.5f);
            }

            if (damaging != null)
            {
                transform.position = _startPos;
            }
        }

        private void AgentMove()
        {
            _agent.SetDestination(new Vector3(_randomXPos, _target.position.y, _target.position.z));
            AgentSetRunAnim(true);
        }

        private void AgentSetAnim(string triggerName)
        {
            _agentAnim.SetTrigger(triggerName);
        }

        private void AgentSetRunAnim(bool value)
        {
            _agentAnim.SetBool("Run", value);
        }
    }
}