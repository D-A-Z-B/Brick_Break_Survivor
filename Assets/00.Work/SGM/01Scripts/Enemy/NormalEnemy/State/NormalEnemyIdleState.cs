using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;

namespace BBS.Enemies
{
    public class NormalEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public NormalEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            this.enemy = entity as Enemy;
            TurnManager.Instance.EnemyTurnStartEvent += HandleStartEnemyTurn;
            enemy.OnDestroyEvent += HandleDieEvent;
        }

        private void HandleDieEvent()
        {
            if (TurnManager.Instance != null)
                TurnManager.Instance.EnemyTurnStartEvent -= HandleStartEnemyTurn;
            enemy.OnDestroyEvent -= HandleStartEnemyTurn;
        }

        public override void Enter()
        {
            base.Enter();
        }

        private void HandleStartEnemyTurn()
        {
            if (enemy == null)
            {
                HandleDieEvent();
                return;
            }

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
                if (enemy.CanAttack())
                    enemy.ChangeState("ATTACK");
                else
                    enemy.ChangeState("MOVE");
            }
            else
            {
                EnemySpawnManager.Instance.EnemyCount();
            }
        }
    }
}

