using UnityEngine;

namespace BBS.Enemies
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private Transform projectilePrefab;

        public void SpawnProjectile(Vector3 dir)
        {
            Transform trm = Instantiate(projectilePrefab, transform.position + (dir * 0.75f), Quaternion.identity);
            trm.forward = dir;
        }
    }
}
