using BBS.Entities;
using BBS.FSM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BBS.Enemies
{
    public class NormalEnemy : Enemy
    {
        protected override void Update()
        {
            base.Update();
        }

        public bool CanAttack()
        {
            Collider[] verticalColliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 3) * 0.5f, transform.rotation, whatIsPlayer);
            Collider[] horizontalColliders = Physics.OverlapBox(transform.position, new Vector3(3, 1, 1) * 0.5f, transform.rotation, whatIsPlayer);

            return verticalColliders.Length > 0 || horizontalColliders.Length > 0;
        }
    }
}
