using System.Collections;
using System.Collections.Generic;
using BBS.Bullets;
using Unity.VisualScripting;
using UnityEngine;

namespace BBS.Core {
    public class GameManager : MonoSingleton<GameManager> {
        [SerializeField] private PoolManagerSO poolManager;
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

        private void Update() {
            if (isFever) {
                if (feverStartTime + feverDuration < Time.time) {
                    currentHitCount = 0;
                    isFever = false;
                    Debug.Log("Fever End");
                    return;
                }

                if (currentHitCount % 100 == 0 && !isCloning) {
                    StartCoroutine(CloneBullets());
                    isCloning = true;
                }
            }
        }

        public void IncreaseHitCount() {
            currentHitCount++;
            isCloning = false;
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
