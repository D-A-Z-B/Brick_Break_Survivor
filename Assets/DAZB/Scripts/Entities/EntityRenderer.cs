using BBS.Animators;
using UnityEngine;

namespace BBS.Entities
{
    public class EntityRenderer : AnimateRenderer, IEntityComponent
    {
        protected Entity _entity;

        [field: SerializeField] public float FacingDirection { get; private set; } = 1;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
    }
}
