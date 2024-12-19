using BBS.Bullets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBS.UI.Skills {
    public class SkillCard : MonoBehaviour {
        [SerializeField] private Image skillIcon;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private int cardIndex;
        private BulletDataSO currentBullet;

        public void SetCard(BulletDataSO data) {
            currentBullet = data;

            skillIcon = data.icon;
            titleText.text = data.displayName + "+ " + data.currentLevel + 1;
            descriptionText.text = data.description;
        }

        public void Selection() {
            BulletManager.Instance.AddBullet(currentBullet);
        }
    }
}
