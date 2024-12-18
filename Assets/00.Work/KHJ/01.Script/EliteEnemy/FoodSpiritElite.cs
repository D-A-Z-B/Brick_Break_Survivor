using BBS.Combat;
using UnityEngine;

namespace BBS.Enemies
{
    public class FoodSpiritElite : Enemy
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                Destroy(enemy);
                print(GetCompo<Health>(true).CurrentHealth);
            }
        }
    }
}
