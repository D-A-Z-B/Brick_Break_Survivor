using UnityEngine;

namespace BBS.Bullets.Effects {
    [CreateAssetMenu(menuName = "SO/Bullet/Effect/IncreaseBulletSize")]
    public class IncreaseBulletSize : BulletEffectSO {
        public float increaseScale;
        public bool isPercent;
        private BulletDataSO bullet;

        public override void SetOwner(BulletDataSO bullet)
        {
            this.bullet = bullet;
        }

        public override void ApplyEffect()
        {
            if (isPercent) {
                bullet.currentScale += bullet.currentScale * (float)(increaseScale / 100);
            }
            else {
                bullet.currentScale += increaseScale;
            }
        }
    }
}
