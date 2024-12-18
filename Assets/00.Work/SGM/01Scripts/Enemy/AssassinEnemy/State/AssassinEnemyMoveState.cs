using BBS.Animators;
using BBS.Enemies;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using UnityEngine;

namespace BBS
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

            Vector3 dir = (PlayerManager.Instance.Player.transform.position - enemy.transform.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            {
                dir.x = Mathf.Sign(dir.x);
                dir.z = 0;
            }
            else
            {
                dir.x = 0;
                dir.z = Mathf.Sign(dir.z);
            }

            AssassinEnemy assassinEnemy = enemy as AssassinEnemy;
            if (assassinEnemy.NeedRotate(dir))
                assassinEnemy.transform.forward = dir;
            enemy.mapManager.MoveEntity(new Coord(enemy.transform.position), new Coord(enemy.transform.position + (dir * enemy.data.moveDistance)), EntityType.Enemy);

            if (assassinEnemy.CanAttack())
                enemy.ChangeState("ATTACK");
            else
                enemy.ChangeState("IDLE");
        }
    }
}
