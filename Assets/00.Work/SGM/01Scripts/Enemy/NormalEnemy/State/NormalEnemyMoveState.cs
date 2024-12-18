using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using DG.Tweening;
using UnityEngine;

namespace BBS.Enemies
{
    public class NormalEnemyMoveState : EntityState
    {
        private Enemy enemy;

        public NormalEnemyMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 dir = (enemy.playerTrm.position - enemy.transform.position).normalized;
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

            enemy.transform.DOMove(enemy.transform.position + dir * enemy.data.moveDistance, 0.5f)
                .OnComplete(() =>
                {
                    NormalEnemy normalEnemy = enemy as NormalEnemy;

                    if (normalEnemy.CanAttack())
                        enemy.ChangeState("ATTACK");
                    else
                        enemy.ChangeState("IDLE");
                });
        }
    }
}
