using BBS.Entities;
using BBS.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace BBS.Enemies
{
    public class Enemy : Entity
    {
        public List<StateSO> states;
        public EnemyDataSO data;

        protected StateMachine stateMachine;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();

            stateMachine = new StateMachine(states, this);
        }

        protected virtual void Start()
        {
            stateMachine.Initialize("IDLE");
        }

        protected virtual void Update()
        {
            stateMachine.UpdateFSM();
        }

        public void ChangeState(string newStateName)
        {
            stateMachine.ChangeState(newStateName);
        }
    }
}
