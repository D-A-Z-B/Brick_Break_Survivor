using UnityEngine;

namespace BBS.Bullets {
    public class BowlingBullet : Bullet {

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
