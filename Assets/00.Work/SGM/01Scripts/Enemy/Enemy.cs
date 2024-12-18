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
        //test
        public Player player;

        public List<StateSO> states;
        public EnemyDataSO data;

        public LayerMask whatIsPlayer; 

        protected StateMachine stateMachine;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();

            stateMachine = new StateMachine(states, this);
        }

        protected override void Awake()
        {
            base.Awake();
            player = FindAnyObjectByType<Player>();
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
