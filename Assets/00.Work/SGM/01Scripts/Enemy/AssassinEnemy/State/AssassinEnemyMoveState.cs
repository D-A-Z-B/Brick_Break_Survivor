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

            enemy.Move();

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
