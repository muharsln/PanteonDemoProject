using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace PanteonDemo.Rank
{
    public class RankController : MonoBehaviour
    {
        public static RankController Instance { get; private set; }

        [SerializeField] private GameObject[] _rankList;
        [SerializeField] private Transform _finishGround;
        [SerializeField] private GameObject _boy;

        private List<GameObject> _rank;
        private int _boyRank;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        private void Update()
        {
            if (GameManager.Instance.gameStat != GameManager.GameStat.Paint || GameManager.Instance.gameStat != GameManager.GameStat.Failed)
            {
                RankUpdate();
            }
        }

        private void RankUpdate()
        {
            _rank = _rankList.OrderBy(rank => (rank.transform.position - _finishGround.position).sqrMagnitude).ToList();

            foreach (GameObject item in _rank)
            {
                if (item.transform.position == _boy.transform.position)
                {
                    _boyRank = _rank.IndexOf(item) + 1;
                }
            }
        }

        public int GetBoyRank()
        {
            return _boyRank;
        }
    }
}