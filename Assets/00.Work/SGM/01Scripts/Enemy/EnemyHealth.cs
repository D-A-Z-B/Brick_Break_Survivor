using BBS.Combat;
using BBS.Entities;

namespace BBS.Enemies
{
    public class EnemyHealth : Health
    {
        Enemy enemy;

        public override void Initialize(Entity entity)
        {
            enemy = entity as Enemy;
        }

        private void Awake()
        {
            maxHealth = enemy.data.maxHealth;
        }
    }
}
