using BBS;
using BBS.Animators;
using BBS.Enemies;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;
        public FoodSpiritIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
            TurnManager.Instance.EnemyTurnStartEvent += HandleStartEnemyTurn;
        }

        public override void Enter()
        {
            base.Enter();
        }

        private void HandleStartEnemyTurn()
        {
            if (enemy.IsStun)
            {
                enemy.SetStun(false);
                return;
            }

            currentTurn++;
            CheckChangeState();
        }

        private void CheckChangeState()
        {
            if (currentTurn >= enemy.data.actionTurn)
            {
                currentTurn = 0;
                enemy.ChangeState("MOVE");
            }
            else
            {
                EnemySpawnManager.Instance.EnemyCount();
            }
        }

        public override void Update()
        {
            base.Update();

        }

        public override void Exit()
        {
            Test.Instance.OnChangeTurn -= HandleStartEnemyTurn;
            base.Exit();
        }
    }
}
