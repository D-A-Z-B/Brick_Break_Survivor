using System;
using System.Collections;
using UnityEngine;

namespace BBS
{
    public class TurnManager : MonoSingleton<TurnManager>
    {
        public Action StartPlayerTurnEvent;
        public Action StartEnemyTurnEvent;

        public Action FirstEliteBossEvent;
        public Action SecondEliteBossEvent;
        public Action LastBossEvent;

        [SerializeField] private float enemyTurnChangeTime = 3f;
        [SerializeField] private int firstBossTurn = 20;
        [SerializeField] private int secondBossTurn = 40;
        [SerializeField] private int lastBossTurn = 60;

        private int turnCount = 1;
        public int TurnCount => turnCount;

        private void Start()
        {
            StartPlayerTurnEvent?.Invoke();
        }

        public void EndPlayerTurn()
        {
            StartEnemyTurnEvent?.Invoke();
            StartCoroutine(ChangeTurnTimer());
        }

        private IEnumerator ChangeTurnTimer()
        {
            yield return new WaitForSeconds(enemyTurnChangeTime);
            EndEnemyTurn();
        }

        private void EndEnemyTurn()
        {
            turnCount++;
            if (turnCount == firstBossTurn)
            {
                FirstEliteBossEvent?.Invoke();
            }
            else if (turnCount == secondBossTurn)
            {
                SecondEliteBossEvent?.Invoke();
            }
            else if (turnCount == lastBossTurn)
            {
                LastBossEvent?.Invoke();
            }
            StartPlayerTurnEvent?.Invoke();
        }
    }
}
