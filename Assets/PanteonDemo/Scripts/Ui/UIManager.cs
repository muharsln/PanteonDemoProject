using System.Collections;
using UnityEngine;

namespace PanteonDemo.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject[] _uiPanels;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
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