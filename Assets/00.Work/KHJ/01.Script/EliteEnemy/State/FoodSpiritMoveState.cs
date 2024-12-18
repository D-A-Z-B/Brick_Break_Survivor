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
        private Enemy enemy;
        private int moveCount = 0;
        public FoodSpiritMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();
            enemy.Move(true);
            moveCount++;
            if (moveCount >= 2)
            {
                int rand = Random.Range(1, 11);
                if (rand >= 8)
                    EnemySpawnManager.Instance.ReSpawnEnemy();
                moveCount = 0;
            }

            if (enemy.CanAttack())
                enemy.ChangeState("ATTACK");
            else
                enemy.ChangeState("IDLE");
        }
    }
}
