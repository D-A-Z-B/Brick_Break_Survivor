using UnityEngine;

namespace BBS.Bullets.Effects {
    [CreateAssetMenu(menuName = "SO/Bullet/Effect/IncreaseBulletDamage")]
    public class IncreaseBulletDamage : BulletEffectSO {
        public float increaseDamage;
        public bool isPercent;
        private BulletDataSO bullet;
        
        public override void SetOwner(BulletDataSO bullet)
        {
            this.bullet = bullet;
        }

        public override void ApplyEffect()
        {
            if (isPercent) {
                bullet.currentDamage += bullet.currentDamage * (float)(increaseDamage / 100);
            }
            else {
                bullet.currentDamage += increaseDamage;
            }
        }

    }
}
