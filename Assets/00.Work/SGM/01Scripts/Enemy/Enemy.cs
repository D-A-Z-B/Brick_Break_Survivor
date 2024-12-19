using BBS.Entities;
using BBS.FSM;
using DG.Tweening;
using KHJ.Core;
using System.Collections.Generic;
using UnityEngine;
using StateMachine = BBS.FSM.StateMachine;

namespace BBS.Enemies
{
    public class Enemy : Entity
    {
        public List<StateSO> states;
        public EnemyDataSO data;

        private StateMachine stateMachine;

        [SerializeField] private LayerMask whatIsPlayer;
        private Vector3 curDir;

        private float jumpPower = 1f;
        private bool isStun = false;
        public bool IsStun => isStun;

        public bool IsCantMove = false;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();

            stateMachine = new StateMachine(states, this);

            curDir = EnemyToPlayerDir();
            transform.forward = curDir;

            GetCompo<EnemyHealth>().OnDead += HandleOnDead;
        }

        private void HandleOnDead()
        {
            ChangeState("DEAD");
            mapManager.DestroyEntity(new Coord(transform.position), this);
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

        public void Move(bool isElite = false)
        {
            if (IsCantMove) return;

            if (NeedRotate())
                transform.forward = curDir;

            mapManager.MoveEntity(new Coord(transform.position), new Coord(transform.position + (curDir * data.moveDistance)), EntityType.Enemy, isElite);
        }

        public void DoMoveEnemy(Coord moveCoord, float speed, bool isJump = false)
        {
            if (!isJump)
            {
                transform.DOMove(new Vector3(moveCoord.x, 1, moveCoord.y), speed).SetEase(Ease.Linear).OnComplete(() =>
                {
                    EnemySpawnManager.Instance.EnemyCount();
                });
            }
            else
            {
                transform.DOJump(transform.position + (curDir * data.moveDistance), jumpPower, 1, 0.5f).SetEase(Ease.Linear)
             .OnComplete(() => EnemySpawnManager.Instance.EnemyCount());
            }
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
            Collider[] verticalColliders = Physics.OverlapBox(transform.position, new Vector3(0.9f, 1, data.attakRange * 2 + 1) * 0.5f, transform.rotation, whatIsPlayer);
            Collider[] horizontalColliders = Physics.OverlapBox(transform.position, new Vector3(data.attakRange * 2 + 1, 1, 0.9f) * 0.5f, transform.rotation, whatIsPlayer);

            return verticalColliders.Length > 0 || horizontalColliders.Length > 0;
        }

        private void OnDestroy()
        {
            GetCompo<EnemyHealth>().OnDead -= HandleOnDead;
        }
    }
}
