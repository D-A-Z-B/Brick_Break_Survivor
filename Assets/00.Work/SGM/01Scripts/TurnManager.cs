using System;
using UnityEngine;

namespace BBS
{
    public enum TurnType
    {
        PlayerTurn,
        EnemeyTurn
    }

    public class TurnManager : MonoSingleton<TurnManager>
    {
        public Action EnemyTurnStart;

        public Action EliteBossEvent;
        public Action LastBossEvent;

        [SerializeField] private int firstBossTurn = 20;
        [SerializeField] private int lastBossTurn = 40;

        private TurnType currentTurnType = TurnType.PlayerTurn;

        private int turnCount = 1;
        public int TurnCount => turnCount;

        private bool isBossRound = false;

        public void ChangeTurn(TurnType type)
        {
            currentTurnType = type;
            Debug.Log(currentTurnType);

            if (currentTurnType == TurnType.EnemeyTurn && !isBossRound)
            {
                turnCount++;
                if (turnCount == firstBossTurn)
                {
                    EliteBossEvent?.Invoke();
                }
                else if (turnCount == lastBossTurn)
                {
                    LastBossEvent?.Invoke();
                }
                EnemyTurnStart?.Invoke();
            }
        }

        public void EndBossRound()
        {
            isBossRound = false;
        }
    }
}
