using System.Collections.Generic;
using BBS.Bullets.Effects;
using UnityEngine;

namespace BBS.Bullets {
    [CreateAssetMenu(fileName = "BulletLevelDataSO", menuName = "SO/Bullet/LevelData")]
    public class BulletLevelDataSO : ScriptableObject {
        public List<BulletEffectSO> effectList;

    }
}
