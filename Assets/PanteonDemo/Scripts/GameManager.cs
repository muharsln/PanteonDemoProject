using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PanteonDemo
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        #endregion

        #region GameStat
        public enum GameStat
        {
            Start,
            Play,
            Stop,
            Failed,
            Paint,
            Finish
        }
        public GameStat gameStat;

        public void SetGameStat(GameStat stat)
        {
            this.gameStat = stat;
        }
        #endregion

        public void SetGameFailed()
        {
            gameStat = GameStat.Failed;
            UI.UIManager.Instance.SetPanelVisible(0);
        }
    }
}