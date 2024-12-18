using BBS.Entities;
using BBS.FSM;
using KHJ.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BBS.Enemies
{
    public class Enemy : Entity
    {
        public List<StateSO> states;
        public EnemyDataSO data;

        private StateMachine stateMachine;

        [SerializeField] private LayerMask whatIsPlayer;
        private Vector3 curDir;

        private bool isStun = false;
        public bool IsStun => isStun;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();

            stateMachine = new StateMachine(states, this);

            curDir = EnemyToPlayerDir();
            transform.forward = curDir;
        }

        private Vector3 EnemyToPlayerDir()
        {
            Vector3 dir = (PlayerManager.Instance.Player.transform.position - transform.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            {
                dir.x = Mathf.Sign(dir.x);
                dir.z = 0;
            }
            else
            {
                dir.x = 0;
                dir.z = Mathf.Sign(dir.z);
            }

            return dir;
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

        public void SetStun(bool value)
        {
            isStun = value;
        }

        public void Move()
        {
            if (NeedRotate())
                transform.forward = curDir;

            mapManager.MoveEntity(new Coord(transform.position), new Coord(transform.position + (curDir * data.moveDistance)), EntityType.Enemy);
        }

        public bool NeedRotate()
        {
            Vector3 dir = EnemyToPlayerDir();

            if (curDir == dir)
                return false;
            else
            {
                curDir = dir;
                return true;
            }
        }

        public bool CanAttack()
        {
            Collider[] verticalColliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 3) * 0.4f, transform.rotation, whatIsPlayer);
            Collider[] horizontalColliders = Physics.OverlapBox(transform.position, new Vector3(3, 1, 1) * 0.4f, transform.rotation, whatIsPlayer);

            return verticalColliders.Length > 0 || horizontalColliders.Length > 0;
        }
    }
}
