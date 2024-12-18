using BBS.Animators;
using BBS.Combat;
using BBS.Entities;
using BBS.FSM;
using BBS.Players;
using UnityEngine;

namespace BBS.Enemies
{
    public class NormalEnemyAttackState : EntityState
    {
        Enemy enemy;

        public NormalEnemyAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as Enemy;
        }

        public override void Enter()
        {
            base.Enter();

            ActionData actionData = new ActionData(enemy.data.damage, enemy.transform);
            PlayerManager.Instance.Player.GetCompo<PlayerHealth>().ApplyDamage(actionData);
            Debug.Log("attack");

            enemy.ChangeState("IDLE");
        }
    }
}
