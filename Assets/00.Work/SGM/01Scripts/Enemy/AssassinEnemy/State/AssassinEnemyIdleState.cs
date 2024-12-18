using BBS.Animators;
using BBS.Entities;
using BBS.FSM;

namespace BBS.Enemies
{
    public class AssassinEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public AssassinEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            TurnManager.Instance.EnemyTurnStart += HandleStartEnemyTurn;
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
                if (enemy.CanAttack())
                    enemy.ChangeState("ATTACK");
                else
                    enemy.ChangeState("MOVE");
            }
        }

        public override void Exit()
        {
            TurnManager.Instance.EnemyTurnStart -= HandleStartEnemyTurn;
        }
    }
}
