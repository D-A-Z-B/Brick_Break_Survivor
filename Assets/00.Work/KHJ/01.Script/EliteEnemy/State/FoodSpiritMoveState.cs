using BBS.Animators;
using BBS.Enemies;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritMoveState : EntityState
    {
        private FoodSpiritElite enemy;
        public FoodSpiritMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as FoodSpiritElite;
        }

        public override void Enter()
        {
            base.Enter();
            SoundManager.Instance.PlaySFX("Boss_Move");
            Debug.Log("Move");
            enemy.Move(true);
        }

        public override void Update()
        {
            base.Update();

            if (enemy.eatPlayer != null)
                enemy.eatPlayer.transform.position = enemy.transform.position;

            if (!enemy.IsMoving)
            {
                if (enemy.eatPlayer != null)
                    enemy.ChangeState("ATTACK");
                else
                {
                    enemy.ChangeState("IDLE");
                }
            }
        }
    }
}
