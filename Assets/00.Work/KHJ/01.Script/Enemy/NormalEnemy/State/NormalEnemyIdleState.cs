using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace KHJ.NormalEnemy
{
    public class NormalEnemyIdleState : EntityState
    {
        public NormalEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("EnemyEnter");
        }
    }
}

