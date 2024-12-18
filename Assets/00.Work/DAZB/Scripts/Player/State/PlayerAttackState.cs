using System.Collections;
using System.Collections.Generic;
using BBS.Animators;
using BBS.Bullets;
using BBS.Entities;
using BBS.FSM;
using UnityEngine;

namespace BBS.Players {
    public class PlayerAttackState : EntityState {
        private Player player;

        public PlayerAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            player = entity as Player;
        }

        public override void Enter() {
            base.Enter();

            player.StartCoroutine(AttackRoutine());
        }

/*         private IEnumerator AttackRoutine() {
            for (int i = 0; i < player.ShootAmount; ++i) {
                Bullet bullet;
                if (i == player.ShootAmount - 1) {
                    bullet = player.PoolManager.Pop(player.TPBulletType) as TPBullet;
                    player.SetTPBullet(bullet as TPBullet);
                }
                else {
                    bullet = player.PoolManager.Pop(player.bulletType) as BFBullet;
                }
                
                Vector3 dir = (player.GetArrow().GetLineEndPoint().position - player.transform.position).normalized;

                bullet.Setup(player.transform.position, dir);
                yield return new WaitForSeconds(player.ShootDelayTime);
            }
            player.ChangeState("TPWAIT");
        } */

        private IEnumerator AttackRoutine() {
            for (int i = 0; i < (int)BulletType.END; ++i) {
                List<BulletDataSO> PlayerBulletList = BulletManager.Instance.PlayerBulletList;
                BulletDataSO temp = null;
                for (int j = 0; j < PlayerBulletList.Count; ++j) {
                    if (PlayerBulletList[j].type == (BulletType)i) {
                        temp = PlayerBulletList[j];
                    }
                }
                if (temp == null) continue;
                for (int k = 0; k < temp.ShootAmount; ++k) {
                    Bullet bullet = null;
                    switch ((BulletType)i) {
                        case BulletType.BFB: {
                            bullet = player.PoolManager.Pop(player.bulletType) as BFBullet;
                            break;
                        }
                        case BulletType.THUNDERB: {
                            break;
                        }
                        case BulletType.TPB: {
                            bullet = player.PoolManager.Pop(player.TPBulletType) as TPBullet;
                            player.SetTPBullet(bullet as TPBullet);
                            break;
                        }
                    }

                    Vector3 dir = (player.GetArrow().GetLineEndPoint().position - player.transform.position).normalized;
                    bullet.Setup(player.transform.position, dir);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            player.ChangeState("TPWAIT");
            yield return null;
        }
    }
}
