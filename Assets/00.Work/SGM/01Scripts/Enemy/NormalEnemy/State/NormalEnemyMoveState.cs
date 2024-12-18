using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class NormalEnemyMoveState : EntityState
    {
        Enemy enemy;
        public NormalEnemyMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            Transform trm = enemy.transform;
            enemy.mapManager.MoveEntity(new Coord(trm.position), new Coord(trm.position +Vector3.right), EntityType.Enemy);
            enemy.ChangeState("IDLE");
        }
    }
}
