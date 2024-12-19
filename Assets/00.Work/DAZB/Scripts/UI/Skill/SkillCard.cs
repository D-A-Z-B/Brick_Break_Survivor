using BBS.Bullets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBS.UI.Skills {
    public class SkillCard : MonoBehaviour {
        [SerializeField] private Image skillIcon;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        public int cardIndex;
        private BulletDataSO currentBullet;

        public RectTransform RectTrm => transform as RectTransform;

        public void SetCard(BulletDataSO data) {
            currentBullet = data;

            skillIcon.sprite = data.icon;
            titleText.text = data.displayName + "+ " + (data.currentLevel + 1);
            descriptionText.text = data.description;
        }

        public void Selection() {
            BulletManager.Instance.AddBullet(currentBullet);
        }
    }
}
