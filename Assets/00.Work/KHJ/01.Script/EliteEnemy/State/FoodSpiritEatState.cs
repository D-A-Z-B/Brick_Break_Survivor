using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace KHJ.Enemies
{
    public class FoodSpiritEatState : EntityState
    {
        public FoodSpiritEatState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
    }
}
