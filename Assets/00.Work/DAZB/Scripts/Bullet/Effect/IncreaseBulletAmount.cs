using UnityEngine;

namespace BBS.Bullets.Effects {
    [CreateAssetMenu(menuName = "SO/Bullet/Effect/IncreaseBulletAmount")]
    public class IncreaseBulletAmount : BulletEffectSO {
        public int increaseAmount;
        private BulletDataSO bullet;

        public override void SetOwner(BulletDataSO bullet) {
            this.bullet = bullet;
        }


        public override void ApplyEffect() {
            bullet.ShootAmount += increaseAmount;
        }
    }
}
