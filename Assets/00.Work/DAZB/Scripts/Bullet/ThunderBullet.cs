using BBS.Enemies;
using KHJ.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class ThunderBullet : Bullet {

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);

            float posX = collision.transform.position.x;
            float posY = collision.transform.position.y;

            float[] dx = { 0, 0, -1, 1, -1, -1, 1, 1 };
            float[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };
            for (int i = 0; i < 8; ++i) {
                int nx = (int)(posX + dx[i]);
                int ny = (int)(posY + dy[i]);

                Enemy enemy = MapManager.Instance.GetEnemyInArr(nx, ny);
            }
        }
    }
}
