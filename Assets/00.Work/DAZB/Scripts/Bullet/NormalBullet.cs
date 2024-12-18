using UnityEngine;

namespace BBS.Bullets {
    public class NormalBullet : Bullet {
		[SerializeField] protected float destroyTime;
        protected bool isCollision = false;
        protected float lastCollisionTime;
        protected float startTime;

        public override void Setup(Vector3 position, Vector3 direction) {
            base.Setup(position, direction);
            startTime = Time.time;
        }

        protected override void Update() {
            base.Update();
            if (isCollision == true && lastCollisionTime + destroyTime < Time.time) {
				myPool.Push(this);
			}

			if (isCollision == false && startTime + 4 < Time.time) {
				myPool.Push(this);
			}
        }

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
            isCollision = true;
			lastCollisionTime = Time.time;
        }

        public override void ResetItem()
        {
            base.ResetItem();
            isCollision = false;
        }
    }
}
