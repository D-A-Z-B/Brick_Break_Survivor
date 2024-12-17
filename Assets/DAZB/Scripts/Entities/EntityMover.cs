using System;
using DG.Tweening;
using BBS.Core.StatSystem;
using UnityEngine;

namespace BBS.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        public event Action<Vector2> OnMovement;

        [Header("Collision detect")] 
        [SerializeField] private Transform _groundCheckTrm;
        [SerializeField] private Vector2 _checkerSize;
        [SerializeField] private float _checkDistance;
        [SerializeField] private LayerMask _whatIsGround;

        [Header("Stats")] 
        [SerializeField] private StatSO _moveSpeedStat;
        
        private Rigidbody2D _rbCompo;
        private EntityRenderer _renderer;
        private EntityStat _statCompo;

        private float _moveSpeed = 6f; //todo : 나중에 스텟시스템으로 변경한다.
        private float _movementX;
        private float _moveSpeedMultiplier, _originalGravity;

        [field: SerializeField] public bool CanManualMove { get; set; } = true;
        
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _statCompo = entity.GetCompo<EntityStat>();
            
            _originalGravity = _rbCompo.gravityScale;
            _moveSpeedMultiplier = 1f;
        }
        
        public void AfterInit()
        {
            _moveSpeedStat = _statCompo.GetStat(_moveSpeedStat);
            _moveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _moveSpeedStat.Value; //초기화 한번은 필요해
        }

        private void OnDestroy()
        {
            _moveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float prev)
        {
            _moveSpeed = current;
        }

        private void FixedUpdate()
        {
            if(CanManualMove)
                _rbCompo.linearVelocityX = _movementX * _moveSpeed * _moveSpeedMultiplier;
            
            OnMovement?.Invoke(_rbCompo.linearVelocity);
        }

        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            _renderer.FlipController(xMovement);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;

            _movementX = 0;
        }
        
        public void SetMovementMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityMultiplier(float value) => _rbCompo.gravityScale = value;

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(force, mode);
        }


        #region KnockBack

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately(true);
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }
        
        #endregion
        
        #region  CheckCollision
        
        public virtual bool IsGroundDetected()
            => Physics2D.BoxCast(_groundCheckTrm.position, 
                _checkerSize, 0, Vector2.down, _checkDistance, _whatIsGround);
        
        #endregion
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (_groundCheckTrm != null)
            {
                Vector3 offset = new Vector3(0, _checkDistance * 0.5f);
                Gizmos.DrawWireCube(_groundCheckTrm.position - offset, 
                    new Vector3(_checkerSize.x, _checkDistance, 1f));
            }
        }
#endif
        
    }
}
