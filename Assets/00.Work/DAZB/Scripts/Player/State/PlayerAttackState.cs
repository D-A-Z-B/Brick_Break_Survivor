using System.Collections;
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

        private IEnumerator AttackRoutine() {
            for (int i = 0; i < player.ShootAmount; ++i) {
                Bullet bullet;
                if (i == player.ShootAmount - 1) {
                    bullet = player.PoolManager.Pop(player.TPBulletType) as TPBullet;
                    player.SetTPBullet(bullet as TPBullet);
                }
                else {
                    bullet = player.PoolManager.Pop(player.bulletType) as NormalBullet;
                }
                
                Vector3 dir = (player.GetArrow().GetLineEndPoint().position - player.transform.position).normalized;

                bullet.Setup(player.transform.position, dir);
                yield return new WaitForSeconds(player.ShootDelayTime);
            }
            player.ChangeState("TPWAIT");
        }
    }
}
