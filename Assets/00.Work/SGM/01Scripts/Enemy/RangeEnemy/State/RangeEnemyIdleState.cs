using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;

namespace BBS.Enemies
{
    public class RangeEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public RangeEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
            TurnManager.Instance.EnemyTurnStartEvent += HandleStartEnemyTurn;
            enemy.OnDestroyEvent += HandleDieEvent;
        }

        private void HandleDieEvent()
        {
            TurnManager.Instance.EnemyTurnStartEvent -= HandleStartEnemyTurn;
            enemy.OnDestroyEvent -= HandleStartEnemyTurn;
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
                enemy.ChangeState("ATTACK");
            }
            else
            {
                EnemySpawnManager.Instance.EnemyCount();
            }
        }
    }
}
