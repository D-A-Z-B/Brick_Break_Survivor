using BBS.Animators;
using BBS.Combat;
using BBS.Entities;
using BBS.FSM;
using BBS.Players;

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

            Player player = enemy.player.GetComponent<Player>();

            ActionData actionData = new ActionData(enemy.data.damage, enemy.transform);
            player.GetCompo<PlayerHealth>().ApplyDamage(actionData);

            enemy.ChangeState("IDLE");
        }
    }
}
