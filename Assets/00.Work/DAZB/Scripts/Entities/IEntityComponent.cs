using UnityEngine;

namespace BBS.Entities
{
    public interface IEntityComponent
    {
        public void Initialize(Entity entity);
    }
}
