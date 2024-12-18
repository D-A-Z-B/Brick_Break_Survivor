using BBS.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class BowlingBullet : Bullet {

        [SerializeField] protected float destroyTime;

        protected override void Update() {
            base.Update();

            if (GameManager.Instance.IsFever == true) return;

            if (isCollision == true && lastCollisionTime + destroyTime < Time.time) {
				myPool.Push(this);
			}

			if (isCollision == false && startTime + 4 < Time.time) {
				myPool.Push(this);
			}
        }

        protected override void Awake() {
            base.Awake();
        }
        
        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);

            if (dataSO.currentLevel >= 3) { 
                dataSO.currentSpeed -= dataSO.currentSpeed * (10 / 100);    
            }
            else {
                dataSO.currentSpeed -= dataSO.currentSpeed * (float)((float)20 / 100);
            }
        }

    }
}
