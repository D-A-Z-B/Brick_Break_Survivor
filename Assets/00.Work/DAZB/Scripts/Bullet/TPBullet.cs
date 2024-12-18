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
                Player player = PlayerManager.Instance.Player;
                player.transform.position = player.GetTPBullet().GetTPPoint() + Vector3.up;
                MapManager.Instance.SetPos(new (player.GetTPBullet().GetTPPoint().x, player.GetTPBullet().GetTPPoint().y), EntityType.Player);
                player.cineCamCompo.Follow = player.transform;
                player.SetTPBullet(null);
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
