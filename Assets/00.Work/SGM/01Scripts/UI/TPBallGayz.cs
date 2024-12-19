using BBS.Bullets;
using UnityEngine;
using UnityEngine.UI;

namespace BBS
{
    public class TPBallGayz : MonoBehaviour
    {
        private Slider slider;
        private CanvasGroup group;

        private TPBullet bullet;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            group = GetComponent<CanvasGroup>();
        }

        public void Show(TPBullet bullet)
        {
            this.bullet = bullet;
            group.alpha = 1;
        }

        public void Hide()
        {
            group.alpha = 0;
        }

        private void Update()
        {
            if (bullet == null) return;

            if (bullet.isCollision)
            {
                slider.maxValue = bullet.destroyTime - bullet.collisionCount * 0.05f;
                slider.value = Time.time - bullet.lastCollisionTime;
            }
            else
            {
                slider.maxValue = 4;
                slider.value = Time.time - bullet.startTime;
            }
        }
    }
}
