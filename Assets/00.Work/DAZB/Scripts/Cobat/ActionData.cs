using UnityEngine;

namespace BBS.Combat {
    public struct ActionData {
        public int damage;
        public Transform dealerTrm;

        public ActionData(int damage, Transform dealerTrm = null) {
            this.damage = damage;
            this.dealerTrm = dealerTrm;
        }
    }
}
