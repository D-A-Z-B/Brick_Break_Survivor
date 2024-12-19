using BBS.Animators;
using BBS.Entities;
using BBS.FSM;

namespace BBS.Enemies
{
    public class AssassinEnemyDeadState : EntityState
    {
        public AssassinEnemyDeadState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
    }
}
