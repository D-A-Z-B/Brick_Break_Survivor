using BBS.Combat;
using BBS.Players;
using KHJ.Core;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritElite : Enemy
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                
            }
            else if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                mapManager.DestroyEntity(new Coord(enemy.transform.position));
                Destroy(enemy);

                EnemyHealth health = GetCompo<Health>(true) as EnemyHealth;
                health.CurrentHealth += health.MaxHealth / 10;
            }
        }
    }
}
