using BBS.Animators;
using BBS.Enemies;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace KHJ.Enemies
{
    public class FoodSpiritMoveState : EntityState
    {
        private Enemy enemy;
        public FoodSpiritMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
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
                enemy.ChangeState("IDLE");
        }
    }
}
