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

            if (isCollision == true && lastCollisionTime + (destroyTime - collisionCount * 0.05f) < Time.time) {
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

                if (IsWithinRange(nx, ny)) {
                    Enemy enemy = MapManager.Instance.GetEnemyInArr(nx, ny);
                    if (enemy != null) {
                        SoundManager.Instance.PlaySFX("Thunder_Ball_Spark");
                        enemy?.GetCompo<Health>(true).ApplyDamage(new ActionData((int)(dataSO.currentDamage * 0.5f)));
                    }
                }
            }
        }
        private bool IsWithinRange(int x, int y) {
            int range = MapManager.Instance.range;
            return x >= 0 && x < range && y >= 0 && y < range;
        }

    }
}
