using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Enemies
{
    public class AssassinEnemyIdleState : EntityState
    {
        private int currentTurn = 0;
        private Enemy enemy;

        public AssassinEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
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
                AssassinEnemy assassinEnemy = enemy as AssassinEnemy;

                currentTurn = 0;
                if (assassinEnemy.CanAttack())
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
