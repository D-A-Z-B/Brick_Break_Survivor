using BBS.Combat;
using BBS.Players;
using UnityEngine;

namespace BBS.UI.Skills {
    [CreateAssetMenu(fileName = "StatCardDataSO", menuName = "SO/StatCardData")]
    public class StatCardDataSO : ScriptableObject {
        public StatCardType type;
        public string displayName;
        public string description;
        public Sprite icon;
        public int increaseHp;
        public int increaseDR;
        public int healHp;

        public void ApplyEffect() {
            Player player = PlayerManager.Instance.Player;
            player.GetCompo<Health>(true).CurrentHealth += (int)(player.GetCompo<Health>(true).MaxHealth * ((float)healHp / 100));
            player.GetCompo<Health>(true).MaxHealth += increaseHp;
            player.GetCompo<Health>(true).CurrentHealth += increaseHp;
            player.GetCompo<Health>(true).Dr += increaseDR;
        }
    }
}
