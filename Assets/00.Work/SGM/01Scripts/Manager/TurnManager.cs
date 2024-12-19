using KHJ.Core;
using System;
using UnityEngine;

namespace BBS
{
    public enum TurnType
    {
        PlayerTurn,
        EnemyTurn
    }

    public class TurnManager : MonoSingleton<TurnManager>
    {
        public Action EnemyTurnStart;

        public Action EliteBossEvent;
        public Action LastBossEvent;

        [SerializeField] private int firstBossTurn = 20;
        [SerializeField] private int lastBossTurn = 40;

        public TurnType currentTurnType = TurnType.PlayerTurn;

        private int turnCount = 1;
        public int TurnCount => turnCount;

        private bool isBossRound = false;

        public void ChangeTurn(TurnType type)
        {
            currentTurnType = type;

            if (currentTurnType == TurnType.EnemyTurn && !isBossRound)
            {
                turnCount++;
                if (turnCount == firstBossTurn)
                {
                    isBossRound = true;
                    EliteBossEvent?.Invoke();
                }
                else if (turnCount == lastBossTurn)
                {
                    isBossRound = true;
                    LastBossEvent?.Invoke();
                }

                CameraManager.Instance.StartZoomOut();
            }
            else if (type == TurnType.PlayerTurn)
            {
                LevelManager.Instance.GetExp();
                CameraManager.Instance.StartZoomIn();
            }

            currentTurnType = type;
        }

        public void EndBossRound()
        {
            isBossRound = false;
        }
    }
}
