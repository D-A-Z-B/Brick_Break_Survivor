using BBS.Combat;

namespace BBS.Enemies
{
    public class EnemyHealth : Health
    {
        private void Awake()
        {
            Enemy enemy = GetComponentInParent<Enemy>();
            
            maxHealth = enemy.data.maxHealth;
        }
    }
}
