using BBS.Animators;
using BBS.Entities;
using BBS.FSM;

namespace BBS.Enemies
{
    public class RangeEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public RangeEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            TurnManager.Instance.StartEnemyTurnEvent += HandleChangeTurn;
        }

        private void HandleChangeTurn()
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
                enemy.ChangeState("ATTACK");
            }
        }

        public override void Exit()
        {
            TurnManager.Instance.StartEnemyTurnEvent -= HandleChangeTurn;
        }
    }
}
