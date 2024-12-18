using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritAttackState : EntityState
    {
        public FoodSpiritAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
    }
}
