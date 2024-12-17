using BBS.Animators;
using BBS.Entities;
using UnityEngine;

namespace BBS.FSM
{
    public abstract class EntityState
    {
        protected Entity _entity;
        
        protected AnimParamSO _stateAnimParam;
        protected bool _isTriggered;

        protected EntityRenderer _renderer;
        protected EntityAnimatorTrigger _animTrigger;

        public EntityState(Entity entity, AnimParamSO stateAnimParam)
        {
            _entity = entity;
            _stateAnimParam = stateAnimParam;
            _renderer = entity.GetCompo<EntityRenderer>();
            _animTrigger = entity.GetCompo<EntityAnimatorTrigger>();
        }

        public virtual void Enter()
        {
            _renderer.SetParam(_stateAnimParam, true);
            _isTriggered = false;
            _animTrigger.OnAnimationEnd += AnimationEndTrigger;
        }

        public virtual void Update()
        { }

        public virtual void Exit()
        {
            _renderer.SetParam(_stateAnimParam, false);
            _animTrigger.OnAnimationEnd -= AnimationEndTrigger;
        }

        public virtual void AnimationEndTrigger()
        {
            _isTriggered = true;
        }
    }
}
