using BBS.Entities;
using BBS.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace KHJ.NormalEnemy
{
    public class NormalEnemy : Entity, IPoolable
    {
        public List<StateSO> states;

        private StateMachine stateMachine;

        public PoolTypeSO PoolType { get; set; }

        public GameObject GameObject => gameObject;

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

        public void SetUpPool(Pool pool)
        {
        }

        public void ResetItem()
        {
        }
    }
}
