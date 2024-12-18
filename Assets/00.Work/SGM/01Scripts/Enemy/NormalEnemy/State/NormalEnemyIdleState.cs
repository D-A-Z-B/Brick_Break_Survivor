using BBS.Animators;
using BBS.Enemies;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace KHJ.NormalEnemy
{
    public class NormalEnemyIdleState : EntityState
    {
        private int currentTurn = 0;

        Enemy enemy;

        public NormalEnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            this.enemy = entity as Enemy;
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.W))
            {
                enemy.transform.position += Vector3.up;
            }
        }

        private void CheckChangeState()
        {
            if (currentTurn == enemy.data.moveTurn)
            {
                
            }
        }
    }
}

