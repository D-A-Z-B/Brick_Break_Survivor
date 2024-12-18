using System.Collections;
using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Players {
    public class PlayerAttackState : EntityState {
        private Player player;

        public PlayerAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            player = entity as Player;
        }

        public override void Enter() {
            base.Enter();

            player.ChangeState("IDLE");
        }

        private IEnumerator AttackRoutine() {
            for (int i = 0; i < player.ShootAmount; ++i) {
                
                yield return new WaitForSeconds(player.ShootDelayTime);
            }
            player.ChangeState("IDLE");
        }
    }
}
