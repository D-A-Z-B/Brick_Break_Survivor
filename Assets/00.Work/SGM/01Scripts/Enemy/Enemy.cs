using BBS.Entities;
using BBS.FSM;
using BBS.Players;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace BBS.Enemies
{
    public class Enemy : Entity
    {
        public List<StateSO> states;
        public EnemyDataSO data;

        public LayerMask whatIsPlayer; 

        private StateMachine stateMachine;

        private bool isStun = false;
        public bool IsStun => isStun;

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

        public void SetStun()
        {
            isStun = true;
        }
    }
}
