using System;
using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Players {
    public class PlayerDragState : EntityState {
        private Player player;

        public PlayerDragState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            player = entity as Player;
        }

        public override void Enter() {
            base.Enter();

            player.SetArrowActive(true);
            player.PlayerInput.attackEvent += HandleAttackEvent;
        }

        public override void Exit() {
            base.Exit();

            player.SetArrowActive(false);
            player.PlayerInput.attackEvent -= HandleAttackEvent;
        }

        public override void Update() {
            base.Update();
        }

        private void HandleAttackEvent() {
            player.ChangeState("ATTACK");
        }
    }
}
