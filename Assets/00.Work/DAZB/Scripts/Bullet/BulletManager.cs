using KHJ.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBS.Bullets {
    public class BulletManager : MonoSingleton<BulletManager>{
        [field: SerializeField]  public List<BulletDataSO> PlayerBulletList {get; private set;}
        [field: SerializeField]  public List<PoolTypeSO> PlayerBulletPoolTypeList {get; private set;}


        private void Start() {

            for (int i = 0; i < PlayerBulletList.Count; ++i) {
                foreach (var iter in PlayerBulletList[i].GetEffectByLevel(0).effectList) {
                    iter.SetOwner(PlayerBulletList[i]);
                    iter.ApplyEffect();
                }
            }
        }

        private void OnDestroy() {
            for (int i = 0; i < PlayerBulletList.Count; ++i) {
                PlayerBulletList[i].ShootAmount = 0;
                PlayerBulletList[i].currentDamage = 0;
                PlayerBulletList[i].currentLevel = 0;
            }
        }

        public PoolTypeSO GetPoolType(BulletType type) {
            return PlayerBulletPoolTypeList[(int)type];
        }

        public void AddBullet(BulletDataSO data) {
            if (PlayerBulletList.Contains(data)) {
                LevelUp(data.type, 1);
            }
            else {
                PlayerBulletList[(int)data.type] = data;
            }
        }

        public void LevelUp(BulletType type, int amount) {
            foreach (var iter in PlayerBulletList) {
                if (iter.type == type) {
                    if (iter.currentLevel >= 5) return;

                    Debug.Log(type);
                    iter.currentLevel += amount;
                    Debug.Log(iter.currentLevel);
                    for (int i = 0; i < iter.GetEffectByLevel(iter.currentLevel).effectList.Count; ++i) {
                        if (iter.GetEffectByLevel(iter.currentLevel).effectList[i] != null) {
                            iter.GetEffectByLevel(iter.currentLevel).effectList[i].SetOwner(iter);
                            iter.GetEffectByLevel(iter.currentLevel).effectList[i].ApplyEffect();
                        }
                    }
                }
            }
        }
    }
}
