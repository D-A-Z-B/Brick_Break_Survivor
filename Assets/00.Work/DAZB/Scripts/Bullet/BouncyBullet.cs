using BBS.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class BouncyBullet : Bullet {
        [SerializeField] protected float destroyTime;

        protected override void Update() {
            base.Update();

            if (GameManager.Instance.IsFever == true) return;

            if (isCollision == true && lastCollisionTime + (destroyTime - collisionCount * 0.05f) < Time.time) {
				myPool.Push(this);
			}

			if (isCollision == false && startTime + 4 < Time.time) {
				myPool.Push(this);
			}
        }

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);

            if (dataSO.currentLevel >= 5) { 
                dataSO.currentSpeed += dataSO.currentSpeed * (float)((float)30 / 100);    
            }
            else {
                dataSO.currentSpeed += dataSO.currentSpeed * (float)((float)20 / 100);
            }

            if (dataSO.currentLevel >= 4) {
                dataSO.currentDamage += dataSO.currentDamage * (float)((float)10 / 100); 
            }

            if (dataSO.currentSpeed >= 50) {
                dataSO.currentSpeed = 50;
            }
        }

        public override void ResetItem()
        {
            base.ResetItem();
            dataSO.currentSpeed = dataSO.defaultSpeed;
        }
    }
}
