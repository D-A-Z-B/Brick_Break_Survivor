using UnityEngine;

namespace BBS.Bullets.Effects {
    [CreateAssetMenu(menuName = "SO/Bullet/Effect/IncreaseBulletSpeed")]
    public class IncreaseBulletSpeed : BulletEffectSO {
        public float  increaseSpeed;
        public bool isPercent;

        private BulletDataSO bullet;

        public override void SetOwner(BulletDataSO bullet) {
            this.bullet = bullet;
        }

        public override void ApplyEffect() {
            if (isPercent) {
                bullet.currentSpeed += bullet.currentSpeed * (float)(increaseSpeed / 100);    
            }
            else {
                bullet.currentSpeed += increaseSpeed;
            }
        }
    }
}
