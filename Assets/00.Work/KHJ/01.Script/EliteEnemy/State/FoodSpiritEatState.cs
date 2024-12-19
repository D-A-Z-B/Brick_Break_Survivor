using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritEatState : EntityState
    {
        Enemy enemy;
        public FoodSpiritEatState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            enemy.ChangeState("IDLE");
        }
    }
}
