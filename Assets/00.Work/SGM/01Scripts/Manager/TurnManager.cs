using BBS.Core;
using BBS.Enemies;
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
        public Action TurnStartEvent;
        public Action EnemyTurnStartEvent;

        public Action BossEvent;
        [SerializeField] private int BossTurn = 20;

        public TurnType currentTurnType = TurnType.PlayerTurn;

        private int turnCount = 19;
        public int TurnCount => turnCount;

        public int modifyStatCount { get; set; } = 1;

        public void ChangeTurn(TurnType type)
        {
            currentTurnType = type;

            if (currentTurnType == TurnType.EnemyTurn)
            {
                turnCount++;
                TurnStartEvent?.Invoke();

                if (turnCount == BossTurn) {
                    SoundManager.Instance.PlayBGM("Boss_BackGround");
                    BossEvent?.Invoke();
                }

                GameManager.Instance.ResetHitCount();
                CameraManager.Instance.StartZoomOut();
            }
        }
    }
}
