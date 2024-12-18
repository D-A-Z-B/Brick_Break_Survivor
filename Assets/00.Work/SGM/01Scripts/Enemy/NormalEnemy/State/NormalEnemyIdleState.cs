using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Enemies
{
    public class NormalEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public NormalEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            this.enemy = entity as Enemy;
        }

        public override void Enter()
        {
            Test.Instance.OnChangeTurn += HandleChangeTurn;
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
                NormalEnemy normalEnemy = enemy as NormalEnemy;

                currentTurn = 0;
                if (normalEnemy.CanAttack())
                    enemy.ChangeState("ATTACK");
                else
                    enemy.ChangeState("MOVE");
            }
        }

        public override void Exit()
        {
            Test.Instance.OnChangeTurn -= HandleChangeTurn;
        }
    }
}

