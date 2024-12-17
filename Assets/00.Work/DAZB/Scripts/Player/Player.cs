using System.Collections.Generic;
using BBS.Core.StatSystem;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;
using UnityEngine.UI;

namespace BBS.Players {
    public class Player : Entity {
        [field: SerializeField] public PlayerInputSO PlayerInput {get; private set;}
        [SerializeField] private Arrow playerArrow;
        [SerializeField] private Bullet bulletPrefab;
        [field: SerializeField] public int shootAmount {get; private set;}
        [field: SerializeField] public float shootDelayTime {get; private set;}

        public List<StateSO> states;

        private StateMachine stateMachine;

        protected override void AfterInitialize() {
            base.AfterInitialize();

            stateMachine = new StateMachine(states, this);
        }

        private void Start() {
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
    }
}
