using BBS.Combat;
using BBS.Enemies;
using UnityEngine;

namespace KHJ.Enemies
{
    public class FoodSpiritElite : Enemy
    {
        protected override void Awake()
        {
            print(GetCompo<Health>().CurrentHealth);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                Destroy(enemy);
                print(GetCompo<Health>().CurrentHealth);
            }
        }
    }
}
