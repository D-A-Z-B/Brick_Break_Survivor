using BBS.Core;
using BBS.Players;
using KHJ.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class TPBullet : Bullet {
        [SerializeField] private LayerMask groundLayer;
        private bool completeTp = false;

        [SerializeField] protected float destroyTime;

        protected override void Update()
        {
            base.Update();

            if (completeTp) {
                myPool.Push(this);
            }

			if (startTime + 2 < Time.time) {
                if (GameManager.Instance.IsFever == true) return;
                Player player = PlayerManager.Instance.Player;
                player.ChangeState("IDLE");

				myPool.Push(this);
			}
        }

        public override void ResetItem()
        {
            base.ResetItem();
            completeTp = false;
        }

        public Vector3 GetTPPoint() {
            completeTp = true;
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer)) {
                return hit.collider.gameObject.transform.position;
            }
            return Vector3.zero;
        }
    }
}
