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
        private int moveCount = 0;
        public FoodSpiritMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as FoodSpiritElite;
        }

        public override void Enter()
        {
            base.Enter();
            enemy.Move(true);
            //moveCount++;
            //if (moveCount >= 2)
            //{
            //    moveCount = 0;
            //    int rand = Random.Range(1, 11);
            //    if (rand >= 8)
            //        EnemySpawnManager.Instance.ReSpawnEnemy();
            //}
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

                enemy.ChangeState("IDLE");
            }
        }
    }
}
