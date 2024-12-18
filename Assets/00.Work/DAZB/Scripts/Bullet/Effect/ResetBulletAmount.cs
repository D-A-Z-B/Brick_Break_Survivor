using UnityEngine;

namespace BBS.Bullets.Effects {
    [CreateAssetMenu(menuName = "SO/Bullet/Effect/ResetBulletAmount")]
    public class ResetBulletAmount : BulletEffectSO {
        public int restAmount;
        private BulletDataSO bullet;

        public override void SetOwner(BulletDataSO bullet)
        {
            this.bullet = bullet;
        }

        public override void ApplyEffect()
        {
            bullet.ShootAmount = restAmount;
        }
    }
}
