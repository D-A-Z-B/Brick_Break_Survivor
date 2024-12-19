using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class RangeEnemyDeadState : EntityState
    {
        Enemy enemy;

        public RangeEnemyDeadState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            SoundManager.Instance.PlaySFX("Enemy_dead");
            LevelManager.Instance.CreateExp(enemy.transform.position);
            enemy.mapManager.DestroyEntity(new Coord(enemy.transform.position), enemy);
        }
    }
}
