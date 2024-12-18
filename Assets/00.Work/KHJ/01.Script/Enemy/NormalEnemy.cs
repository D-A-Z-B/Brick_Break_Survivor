using BBS.Entities;
using BBS.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace KHJ
{
    public class NormalEnemy : Entity
    {
        public List<StateSO> states;

        private StateMachine stateMachine;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();

            stateMachine = new StateMachine(states, this);
        }

        private void Start()
        {
            stateMachine.Initialize("IDLE");
        }

        private void Update()
        {
            stateMachine.UpdateFSM();
        }

        public void ChangeState(string newStateName)
        {
            stateMachine.ChangeState(newStateName);
        }
    }
}
