using UnityEngine;

namespace BBS.Bullets {
    public class BouncyBullet : Bullet {
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
        }
    }
}
