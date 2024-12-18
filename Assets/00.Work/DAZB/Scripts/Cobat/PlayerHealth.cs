using BBS.Entities;
using BBS.Players;
using UnityEngine;

namespace BBS.Combat {
    public class PlayerHealth : Health
    {
        private Player player;
        public override void Initialize(Entity entity)
        {
            player = entity as Player;
        }
    }
}
