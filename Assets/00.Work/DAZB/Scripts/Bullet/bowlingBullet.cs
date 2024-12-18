using UnityEngine;

namespace BBS.Bullets{
    public class bowlingBullet : Bullet {
        private float defaultSpeed;

        protected override void Awake() {
            base.Awake();
            defaultSpeed = speed;
        }
        
        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);

            if (dataSO.currentLevel >= 3) { 
                speed -= speed * (10 / 100);    
            }
            else {
                speed -= speed * (float)((float)20 / 100);
            }
        }

        public override void ResetItem()
        {
            base.ResetItem();
            speed = defaultSpeed;
        }
    }
}
