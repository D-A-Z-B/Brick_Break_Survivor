using System;
using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Players {
    public class PlayerIdleState : EntityState {
        private Player player;

        public PlayerIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            player = entity as Player;
        }

        public override void Enter() {
            base.Enter();

            player.PlayerInput.attackDragEvent += HandleAttackDragEvent;
        }

        public override void Exit() {
            base.Exit();

            player.PlayerInput.attackDragEvent -= HandleAttackDragEvent;
        }

         private void HandleAttackDragEvent() {
            if (TurnManager.Instance.currentTurnType != TurnType.PlayerTurn) {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Player"))) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
                    player.ChangeState("DRAG");
                }
            }
        }

    }
}
