using BBS.Enemies;
using KHJ.Core;
using UnityEngine;

namespace BBS.Bullets {
    public class ThunderBullet : Bullet {

        protected override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);

            float posX = collision.transform.position.x;
            float posY = collision.transform.position.y;

            if (dataSO.currentLevel >= 5) {
                // 적 스턴 코드 넣을 거임
            }

            float[] dx = { 0, 0, -1, 1, -1, -1, 1, 1 };
            float[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };
            for (int i = 0; i < 8; ++i) {
                int nx = (int)(posX + dx[i]);
                int ny = (int)(posY + dy[i]);

                Enemy enemy = MapManager.Instance.GetEnemyInArr(nx, ny);
                enemy.GetCompo<EnemyHealth>().ApplyDamage(new Combat.ActionData((int)(dataSO.currentDamage * ((float)1/2))));
            }
        }
    }
}
