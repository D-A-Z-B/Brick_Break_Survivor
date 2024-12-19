using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;

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
        }

        public override void Update()
        {
            base.Update();
            if(!enemy.IsMoving)
            {
                if (enemy.CanAttack())
                    enemy.ChangeState("ATTACK");
                else
                {
                    EnemySpawnManager.Instance.EnemyCount();
                    enemy.ChangeState("IDLE");
                }
            }
        }
    }
}
