using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BBS.Bullets {
    [CreateAssetMenu(fileName = "BulletDataSO", menuName = "SO/BulletData")]
    public class BulletDataSO : ScriptableObject {
        public Image icon;
        public string displayName;
        public string description;
        public float currentDamage;
        public float defaultDamage;
        public float currentScale;
        public float defaultScale;
        public float currentSpeed;
        public float defaultSpeed = 10;
        public int currentLevel = 0;
        public int ShootAmount;
        public List<BulletLevelDataSO> levelDataList;
        public BulletType type;

        private void OnEnable() {
            currentDamage = defaultDamage;
            currentScale = defaultScale;
            currentSpeed = defaultSpeed;
        }

        private void OnDisable() {
            currentDamage = defaultDamage;
            currentScale = defaultScale;
            currentSpeed = defaultSpeed;
        }

        public BulletLevelDataSO GetEffectByLevel(int level) {
            return levelDataList[level];
        }
    }
}
