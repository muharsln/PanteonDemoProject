using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace PanteonDemo.UI
{
    public class UICounter : MonoBehaviour
    {
        private int _timeAmount;
        private int _startAmount;

        [SerializeField] private TextMeshProUGUI _startText;
        [SerializeField] private TextMeshProUGUI _timeText;
        private void Start()
        {
            _startAmount = Convert.ToInt32(_startText.text);
            _timeAmount = Convert.ToInt32(_timeText.text);
            StartCoroutine(StartCounter());
        }

        private IEnumerator TimeCounter()
        {
            while (_timeAmount > -1 && GameManager.Instance.gameStat == GameManager.GameStat.Play || GameManager.Instance.gameStat == GameManager.GameStat.Stop)
            {
                if (_timeAmount <= 0)
                {
                    GameManager.Instance.SetGameFailed();
                }

                yield return new WaitForSeconds(1);

                if (_timeAmount > 0 && GameManager.Instance.gameStat != GameManager.GameStat.Paint)
                {
                    _timeAmount--;
                    _timeText.text = _timeAmount.ToString();
                    _timeText.transform.DOScale(_timeText.transform.localScale * 1.2f, 0.2f).OnComplete(() => _timeText.transform.DOScale(_timeText.transform.localScale / 1.2f, 0.2f));
                }
            }
        }
        private IEnumerator StartCounter()
        {
            while (_startAmount > 0 && GameManager.Instance.gameStat == GameManager.GameStat.Start)
            {
                yield return new WaitForSeconds(1);
                _startAmount--;
                _startText.text = _startAmount.ToString();
                _startText.transform.DOScale(_startText.transform.localScale * 1.2f, 0.2f).OnComplete(() => _startText.transform.DOScale(_startText.transform.localScale / 1.2f, 0.2f));

                if (_startAmount <= 0)
                {
                    GameManager.Instance.SetGameStat(GameManager.GameStat.Play);
                    StartCoroutine(TimeCounter());
                    _startText.enabled = false;
                    _timeText.enabled = true;
                }
            }
        }
    }
}