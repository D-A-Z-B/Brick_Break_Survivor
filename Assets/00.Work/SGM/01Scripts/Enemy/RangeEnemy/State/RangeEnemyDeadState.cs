using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Enemies
{
    public class RangeEnemyDeadState : EntityState
    {
        public RangeEnemyDeadState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
    }
}
