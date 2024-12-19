using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;

namespace BBS.Enemies
{
    public class AssassinEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public AssassinEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
            enemy.OnDestroyEvent += HandleDieEvent;
            TurnManager.Instance.EnemyTurnStartEvent += HandleStartEnemyTurn;
        }

        private void HandleDieEvent()
        {
            TurnManager.Instance.EnemyTurnStartEvent -= HandleStartEnemyTurn;
            enemy.OnDestroyEvent -= HandleStartEnemyTurn;
        }

        private void HandleStartEnemyTurn()
        {
            if(enemy == null)
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
