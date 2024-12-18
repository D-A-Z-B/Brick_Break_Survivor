using BBS.Combat;
using BBS.Core;
using BBS.Enemies;
using KHJ.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class ThunderBullet : Bullet {

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

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);

            float posX = collision.transform.position.x;
            float posY = collision.transform.position.y;

            if (dataSO.currentLevel >= 5) {
                int rand = Random.Range(1, 10);
                if (rand == 1) {
                    if (TryGetComponent<Enemy>(out Enemy enemy)) {
                        enemy.SetStun(true);
                    }
                }
            }

            float[] dx = { 0, 0, -1, 1, -1, -1, 1, 1 };
            float[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };
            for (int i = 0; i < 8; ++i) {
                int nx = (int)(posX + dx[i]);
                int ny = (int)(posY + dy[i]);

                Enemy enemy = MapManager.Instance.GetEnemyInArr(nx, ny);
                enemy?.GetCompo<Health>(true).ApplyDamage(new ActionData((int)(dataSO.currentDamage * ((float)1/2))));
            }
        }
    }
}
