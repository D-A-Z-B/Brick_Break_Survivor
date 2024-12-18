using UnityEngine;

namespace BBS.Enemies
{
    public class AssassinEnemy : Enemy
    {
        private Vector3 curDir;
        public Vector3 CurDir => curDir;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();

            Vector3 dir = (PlayerManager.Instance.Player.transform.position - transform.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            {
                dir.x = Mathf.Sign(dir.x);
                dir.z = 0;
            }
            else
            {
                dir.x = 0;
                dir.z = Mathf.Sign(dir.z);
            }

            curDir = dir;
            transform.forward = curDir;
        }

        public bool NeedRotate(Vector3 dir)
        {
            if (curDir == dir)
                return false;
            else
            {
                curDir = dir;
                return true;
            }
        }

        public bool CanAttack()
        {
            Collider[] verticalColliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 3) * 0.5f, transform.rotation, whatIsPlayer);
            Collider[] horizontalColliders = Physics.OverlapBox(transform.position, new Vector3(3, 1, 1) * 0.5f, transform.rotation, whatIsPlayer);

            return verticalColliders.Length > 0 || horizontalColliders.Length > 0;
        }
    }
}
