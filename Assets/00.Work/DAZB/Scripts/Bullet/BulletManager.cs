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
                if (PlayerBulletList[i] == null) continue;
                LevelUp(PlayerBulletList[i].type, 1);
            }
        }

        private void OnDestroy() {
            for (int i = 0; i < PlayerBulletList.Count; ++i) {
                if (PlayerBulletList[i] == null) continue;
                PlayerBulletList[i].ShootAmount = 0;
                PlayerBulletList[i].currentDamage = 0;
                PlayerBulletList[i].currentLevel = -1;
            }
        }

        public void AddBullet(BulletDataSO data) {
            if (PlayerBulletList.Contains(data)) {
                LevelUp(data.type, 1);
            }
            else {
                PlayerBulletList[(int)data.type] = data;
                LevelUp(data.type, 1);
            }
        }

        public PoolTypeSO GetPoolType(BulletType type) {
            return PlayerBulletPoolTypeList[(int)type];
        }

        public void LevelUp(BulletType type, int amount) {
            foreach (var iter in PlayerBulletList) {
                if (iter == null) continue;
                if (iter.type == type) {
                    if (iter.currentLevel >= 5) return;

                    iter.currentLevel += amount;
                    for (int i = 0; i < iter.GetEffectByLevel(iter.currentLevel).effectList.Count; ++i) {
                        if (iter.GetEffectByLevel(iter.currentLevel).effectList[i] != null) {
                            iter.GetEffectByLevel(iter.currentLevel).effectList[i].SetOwner(iter);
                            iter.GetEffectByLevel(iter.currentLevel).effectList[i].ApplyEffect();
                        }
                    }
                }
            }
        }

        public bool CanShoot() {
            // 턴 시스템 만들면 수정함
            return true;
        }
/* 
        public bool IsShootComplete() {
            return !isExecuteShootRoutine;
        }

        public void StartShootRoutine() {
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine() {
            isExecuteShootRoutine = true;

            for (int i = 0; i < (int)BulletType.END; ++i) {
                BulletDataSO temp = null;
                for (int j = 0; j < PlayerBulletList.Count; ++j) {
                    if (PlayerBulletList[j].type == (BulletType)i) {
                        temp = PlayerBulletList[j];
                    }
                }

                for (int k = 0; i < temp.ShootAmount; ++k) {
                    
                    yield return new WaitForSeconds(0.1f);
                }
            }

            yield return null;
        } */
    }
}
