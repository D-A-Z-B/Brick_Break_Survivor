using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class RangeEnemyAttackState : EntityState
    {
        private RangeEnemy enemy;

        public RangeEnemyAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as RangeEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            SoundManager.Instance.PlaySFX("RangeEnemy_Attack");
            Vector3[] spawnDirs = { Vector3.right, Vector3.left, Vector3.forward, -Vector3.forward};
            foreach (Vector3 dir in spawnDirs)
                enemy.SpawnProjectile(dir);

            EnemySpawnManager.Instance.EnemyCount();
            enemy.ChangeState("IDLE");
        }
    }
}
