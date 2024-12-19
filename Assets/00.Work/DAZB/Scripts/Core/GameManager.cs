using System.Collections;
using System.Collections.Generic;
using BBS.Bullets;
using BBS.Players;
using BBS.UI;
using KHJ.Core;
using UnityEngine;

namespace BBS.Core {
    public class GameManager : MonoSingleton<GameManager> {
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolTypeSO hitCountText;
        public int startFeverHitCount;
        public float feverDuration;
        private List<Bullet> spawnedBullet = new List<Bullet>();

        private int currentHitCount;

        private float feverStartTime;

        private bool isFever;
        public bool IsFever {
            get => isFever;
            set => isFever = value;
        }

        private bool isCloning = false;

        private int feverCount = 1;

        private void Update() {
            if (isFever) {
                if (feverStartTime + feverDuration < Time.time) {
                    isFever = false;
                    List<Bullet> temp = new List<Bullet>(spawnedBullet);
                    spawnedBullet.Clear();
                    foreach (Bullet iter in temp) {
                        iter.ForcePush();
                    }
                    currentHitCount = 0;

                    if (PlayerManager.Instance.Player != null) {
                        PlayerManager.Instance.Player.ChangeState("IDLE");
                    }

                    Debug.Log("Fever End");
                    return;
                }

                if (currentHitCount > 0 && currentHitCount % startFeverHitCount == 0 && !isCloning) {
                    StartCoroutine(CloneBullets());
                    feverCount++;
                    isCloning = true;
                }
            }
        }

        public void IncreaseHitCount() {
            currentHitCount++;
            isCloning = false;
            HitCountUI text = poolManager.Pop(hitCountText) as HitCountUI;
            if (isFever == false) {
                text.SetText("hit* " + currentHitCount);
            }
            else {
                text.SetText("fever* " + feverCount);
            }
            if (currentHitCount >= startFeverHitCount && isFever == false) {
                isFever = true;
                feverStartTime = Time.time;
                StartCoroutine(CloneBullets());
                Debug.Log("Fever Start");
            }

            Debug.Log("Hit: " + currentHitCount);
        }

        private IEnumerator CloneBullets() {
            isCloning = true;
            List<Bullet> temp = new List<Bullet>(spawnedBullet);

            for (int i = 0; i < temp.Count; ++i) {
                if (isFever == false) break;
                if (temp[i].dataSO.type == BulletType.TPB) continue;
                Debug.Log("Clone : " + temp[i].dataSO.type);
                Bullet bullet = poolManager.Pop(BulletManager.Instance.GetPoolType(temp[i].dataSO.type)) as Bullet;
                if (bullet == null) continue; 

                Vector3 dir = Random.insideUnitSphere;
                dir.y = 0;
                bullet.Setup(temp[i].transform.position, dir);
                yield return null;
            }
            isCloning = false;
        }



        public void AddBullet(Bullet bullet) {
            spawnedBullet.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet) {
            spawnedBullet.Remove(bullet);
        }
    }
}
