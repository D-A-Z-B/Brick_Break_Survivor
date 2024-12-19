using System.Collections.Generic;
using BBS.Bullets;
using BBS.Combat;
using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using Unity.Cinemachine;
using UnityEngine;

namespace BBS.Players {
    public class Player : Entity {
        [field: SerializeField] public PlayerInputSO PlayerInput {get; private set;}
        [SerializeField] private Arrow playerArrow;
        [field: SerializeField] public CinemachineVirtualCameraBase cineCamCompo {get; private set;}
        [field: SerializeField] public PoolManagerSO PoolManager {get; private set;}

        private TPBullet tPBullet;

        public List<StateSO> states;

        private StateMachine stateMachine;

        public bool IsDead {get; private set;}

        protected override void AfterInitialize() {
            base.AfterInitialize();

            GetCompo<Health>(true).OnDead += ()=> IsDead = true;

            stateMachine = new StateMachine(states, this);
        }

        private void Start() {
            //MapManager.Instance.SetPos(new (MapManager.Instance.range / 2, MapManager.Instance.range / 2), EntityType.Player);
            //transform.position = new (MapManager.Instance.range / 2, 1, MapManager.Instance.range / 2);
            MapManager.Instance.SetPos(new(MapManager.Instance.range / 2, MapManager.Instance.range / 2 - 6), EntityType.Player);
            transform.position = new(MapManager.Instance.range / 2, 1, MapManager.Instance.range / 2 - 6);
            stateMachine.Initialize("IDLE");
        }

        private void Update() {

            stateMachine.UpdateFSM();
        }

        public void ChangeState(string newStateName) {
            stateMachine.ChangeState(newStateName);
        }

        public void SetArrowActive(bool isActive) {
            playerArrow.gameObject.SetActive(isActive);
        }

        public Arrow GetArrow() {
            return playerArrow;
        }

        public void SetTPBullet(TPBullet bullet) {
            tPBullet = bullet;
        }

        public TPBullet GetTPBullet() {
            return tPBullet;
        }
    }
}
