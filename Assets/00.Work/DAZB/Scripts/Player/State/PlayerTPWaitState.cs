using BBS.Animators;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using Unity.Cinemachine;
using UnityEngine;

namespace BBS.Players {
    public class PlayerTPWaitState : EntityState {
        private Player player;

        public PlayerTPWaitState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            player = entity as Player;
        }

        public override void Enter()
        {
            base.Enter();
            player.cineCamCompo.Follow = player.GetTPBullet().transform;
            player.PlayerInput.tpEvent += HandleTPEvent;
        }

        public override void Exit()
        {
            player.PlayerInput.tpEvent -= HandleTPEvent;
            base.Exit();
        }

        private void HandleTPEvent() {
            player.transform.position = player.GetTPBullet().GetTPPoint() + Vector3.up;
            MapManager.Instance.SetPos(new (player.GetTPBullet().GetTPPoint().x, player.GetTPBullet().GetTPPoint().y), EntityType.Player);
            player.cineCamCompo.Follow = player.transform;
            player.SetTPBullet(null);
            player.ChangeState("IDLE");
        }
    }
}
