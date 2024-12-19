using BBS.Combat;
using BBS.Players;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritElite : Enemy
    {
        [HideInInspector] public Player eatPlayer;
      public float forceDuration;
       public AnimationCurve forceEase;

        private void OnCollisionEnter(Collision collision)
        {
            print(collision);
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                eatPlayer = player;
                mapManager.DestroyEntity(new Coord(eatPlayer.transform.position), player);
                print("¸ÂÀ½");
            }

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                mapManager.DestroyEntity(new Coord(enemy.transform.position), enemy, true);

                EnemyHealth health = GetCompo<Health>(true) as EnemyHealth;
                health.CurrentHealth += health.MaxHealth / 10;
            }
        }
    }
}
