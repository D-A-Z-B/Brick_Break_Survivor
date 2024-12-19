using System.Collections;
using System.Collections.Generic;
using BBS.Bullets;
using BBS.Players;
using BBS.UI;
using KHJ.Core;
using UnityEngine;

namespace BBS.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolTypeSO hitCountText;
        public int startFeverHitCount;
        public float feverDuration;
        private List<Bullet> spawnedBullet = new List<Bullet>();

        private int currentHitCount;
        private int maxHitCount = 0;

        private float feverStartTime;

        private bool isFever;
        public bool IsFever
        {
            get => isFever;
            set => isFever = value;
        }

        private bool isCloning = false;

        private int feverCount;

        private void Update()
        {
            if (isFever)
            {
                if (feverStartTime + feverDuration < Time.time)
                {
                    // spawnedBullet 복사본 사용 후 순회
                    isFever = false;
                    List<Bullet> temp = new List<Bullet>(spawnedBullet);
                    spawnedBullet.Clear();
                    foreach (Bullet iter in temp)
                    {
                        iter.ForcePush();
                    }

                    // 모든 Bullet 정리
                    currentHitCount = 0;

                    // 플레이어 상태 변경
                    if (PlayerManager.Instance.Player != null)
                    {
                        PlayerManager.Instance.Player.ChangeState("IDLE");
                    }

                    Debug.Log("Fever End");
                    return;
                }

                // CloneBullets 호출 조건 개선
                if (currentHitCount > 0 && currentHitCount % startFeverHitCount == 0 && !isCloning)
                {
                    StartCoroutine(CloneBullets());
                    isCloning = true;
                }
            }
        }


        public void IncreaseHitCount()
        {
            currentHitCount++;
            isCloning = false;
            HitCountUI text = poolManager.Pop(hitCountText) as HitCountUI;
            if (isFever == false) {
                if (currentHitCount >= 1) {
                    text.SetText("<#FF6347>hit* " + currentHitCount +"</color>");
                }
                else {
                    text.SetText("hit* " + currentHitCount);
                }
            }
            else {
                text.SetText("fever* " + feverCount);
            }
            if (currentHitCount >= startFeverHitCount && isFever == false)
            {
                isFever = true;
                feverStartTime = Time.time;
                StartCoroutine(CloneBullets());
                Debug.Log("Fever Start");
            }

            Debug.Log("Hit: " + currentHitCount);
        }

        private IEnumerator CloneBullets()
        {
            isCloning = true;
            List<Bullet> temp = new List<Bullet>(spawnedBullet);

            for (int i = 0; i < temp.Count; ++i)
            {
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

        public void ResetHitCount()
        {
            if(currentHitCount > maxHitCount)
                maxHitCount = currentHitCount;
            currentHitCount = 0;
        }

        public float GetMaxHitCount() => maxHitCount;

        public void AddBullet(Bullet bullet)
        {
            spawnedBullet.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            spawnedBullet.Remove(bullet);
        }
    }
}
