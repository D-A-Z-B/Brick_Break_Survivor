using BBS.Animators;
using BBS.Combat;
using BBS.Entities;
using BBS.FSM;
using BBS.Players;
using DG.Tweening;
using KHJ.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritAttackState : EntityState
    {
        private FoodSpiritElite enemy;
        public FoodSpiritAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            enemy = entity as FoodSpiritElite;
        }

        public override void Enter()
        {
            base.Enter();
            SoundManager.Instance.PlaySFX("Boss_Attack");
            PlayerManager.Instance.Player.GetCompo<Health>().CurrentHealth -= 100;

            Player player = enemy.eatPlayer;

            player.transform.DOMove(CheckShot(), enemy.forceDuration).SetEase(enemy.forceEase).
            OnComplete(() =>
            {
                enemy.mapManager.SetPos(new Coord(player.transform.position), EntityType.Player);
                enemy.eatPlayer = null;
                EnemySpawnManager.Instance.EnemyCount();
                enemy.ChangeState("IDLE");
            });
        }

        private bool MapCondition(Vector3 pos)
        {
            return pos.x >= 0 && pos.x < enemy.mapManager.range &&
              pos.z >= 0 && pos.z < enemy.mapManager.range;
        }

        private Vector3 CheckShot()
        {
            Vector3 lastPos = Vector3.zero;
            EntityType goPosType = EntityType.Empty;
            for (int i = 2; i <= 6; i++)
            {
                Vector3 newPosition = enemy.transform.position + enemy.transform.forward * i;

                if (!MapCondition(newPosition) && i == 2)
                {
                    enemy.transform.rotation = Quaternion.LookRotation(-enemy.transform.forward);
                    return CheckShot();
                }

                if (MapCondition(newPosition))
                    goPosType = enemy.mapManager.GetPos(new Coord(newPosition));
                else
                {
                    PlayerManager.Instance.Player.GetCompo<Health>().CurrentHealth -= 20;
                    return lastPos;
                }

                if (goPosType == EntityType.Enemy)
                {
                    PlayerManager.Instance.Player.GetCompo<Health>().CurrentHealth -= 20;
                    lastPos = newPosition;
                    return lastPos;
                }

                if (goPosType == EntityType.Empty)
                {
                    lastPos = newPosition;
                    continue;
                }
            }
            return lastPos;
        }
    }
}
