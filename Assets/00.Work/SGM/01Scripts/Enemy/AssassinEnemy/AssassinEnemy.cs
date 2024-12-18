using UnityEngine;

namespace BBS.Enemies
{
    public class AssassinEnemy : Enemy
    {
        public bool CanAttack()
        {
            Collider[] verticalColliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 3) * 0.5f, transform.rotation, whatIsPlayer);
            Collider[] horizontalColliders = Physics.OverlapBox(transform.position, new Vector3(3, 1, 1) * 0.5f, transform.rotation, whatIsPlayer);

            return verticalColliders.Length > 0 || horizontalColliders.Length > 0;
        }
    }
}
