using BBS;
using BBS.Animators;
using BBS.Enemies;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Enemies 
{
    public class FoodSpiritIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;
        public FoodSpiritIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy; 
        }

        public override void Enter()
        {
            base.Enter();
            Test.Instance.OnChangeTurn += HandleChangeTurn;
            enemy.transform.rotation = Quaternion.identity;
        }

        private void HandleChangeTurn()
        {
            currentTurn++;
            Debug.Log(currentTurn);
            CheckChangeState();
        }

        private void CheckChangeState()
        {
            if (currentTurn >= enemy.data.actionTurn)
            {
                FoodSpiritElite foodSpiritElite = enemy as FoodSpiritElite;

                currentTurn = 0;
                if (foodSpiritElite.CanAttack())
                    enemy.ChangeState("ATTACK");
                else
                    enemy.ChangeState("MOVE");
            }
        }

        public override void Update()
        {
            base.Update();

        }

        public override void Exit()
        {
            Test.Instance.OnChangeTurn -= HandleChangeTurn;
            base.Exit();
        }
    }
}
