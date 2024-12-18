using BBS.Players;
using KHJ.Core;
using UnityEngine;

namespace BBS
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        [SerializeField] private Player player;
        public Player Player => player;
    }
}
