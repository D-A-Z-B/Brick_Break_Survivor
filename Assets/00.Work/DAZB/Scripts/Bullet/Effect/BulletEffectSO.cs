using UnityEngine;

namespace BBS.Bullets.Effects {

    public abstract class BulletEffectSO : ScriptableObject {
        public abstract void SetOwner(BulletDataSO bullet);
        public abstract void ApplyEffect();
    }
}
