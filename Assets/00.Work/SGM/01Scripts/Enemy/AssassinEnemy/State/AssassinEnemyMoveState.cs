using BBS.Animators;
using BBS.Entities;
using BBS.FSM;

namespace BBS.Enemies
{
    public class AssassinEnemyMoveState : EntityState
    {
        private Enemy enemy;

        public AssassinEnemyMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();

            enemy.Move();

            if (enemy.CanAttack())
                enemy.ChangeState("ATTACK");
            else
                enemy.ChangeState("IDLE");
        }
    }
}
