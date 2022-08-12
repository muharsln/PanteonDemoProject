using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace PanteonDemo.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject[] _uiPanels;

        [SerializeField] private TextMeshProUGUI _rankText;

        [SerializeField] private int _num;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Update()
        {
            if (GameManager.Instance.gameStat != GameManager.GameStat.Paint || GameManager.Instance.gameStat != GameManager.GameStat.Failed)
            {
                if (Rank.RankController.Instance.GetBoyRank() != _num)
                {
                    _num = Rank.RankController.Instance.GetBoyRank();
                    _rankText.transform.DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(() =>
                    {
                        _rankText.text = _num.ToString() + " / 10";
                        _rankText.transform.DOScale(_rankText.transform.localScale / 1.2f, 0.1f);
                    });
                }
            }
        }

        /// <summary>
        /// - Gönderilen deðer 0 ise GameOverPanel'i açar. 
        /// - Gönderilen deðer 1 ise GameFinishPanel'i açar.
        /// </summary>
        public void SetPanelVisible(byte panelNum)
        {
            StartCoroutine(DelayPanelVisiable(panelNum));
        }

        private IEnumerator DelayPanelVisiable(byte panelNum)
        {
            yield return new WaitForSeconds(2f);
            _uiPanels[panelNum].gameObject.SetActive(true);
        }
    }
}