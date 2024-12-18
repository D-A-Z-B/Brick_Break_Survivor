using BBS.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class BFBullet : Bullet {
		[SerializeField] protected float destroyTime;

        public override void Setup(Vector3 position, Vector3 direction) {
            base.Setup(position, direction);
        }

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

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
        }

        public override void ResetItem()
        {
            base.ResetItem();
        }
    }
}
