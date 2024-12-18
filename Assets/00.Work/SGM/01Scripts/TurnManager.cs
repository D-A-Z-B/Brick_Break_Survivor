using DG.Tweening;
using KHJ.Core;
using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace BBS
{
    public enum TurnType
    {
        PlayerTurn,
        EnemyTurn
    }

    public class TurnManager : MonoSingleton<TurnManager>
    {
        public Action EnemyTurnStart;

        public Action EliteBossEvent;
        public Action LastBossEvent;

        [Header("cam")]
        [SerializeField] private CinemachineCamera cam;
        [SerializeField] private float ZoomInYPos = 140;

        [Header("turn")]
        [SerializeField] private int firstBossTurn = 20;
        [SerializeField] private int lastBossTurn = 40;

        private TurnType currentTurnType = TurnType.PlayerTurn;

        private Vector3 originCamPos;
        private int turnCount = 1;
        public int TurnCount => turnCount;

        private bool isBossRound = false;

        public void ChangeTurn(TurnType type)
        {
            if (type == TurnType.EnemyTurn && !isBossRound)
            {
                turnCount++;
                if (turnCount == firstBossTurn)
                {
                    EliteBossEvent?.Invoke();
                }
                else if (turnCount == lastBossTurn)
                {
                    LastBossEvent?.Invoke();
                }

                StartCoroutine(CamZoomIn());
            }
            else if(type == TurnType.PlayerTurn)
            {
                StartCoroutine(CamZoomOut());
            }

            currentTurnType = type;
        }

        private IEnumerator CamZoomIn()
        {
            float timer = 0;

            cam.Follow = null;
            originCamPos = cam.transform.position;
            while (true)
            {
                timer += Time.deltaTime;

                float lerpX = Mathf.Lerp(cam.transform.position.x, MapManager.Instance.range / 2, timer);
                float lerpY = Mathf.Lerp(cam.transform.position.y, ZoomInYPos, timer);
                float lerpZ = Mathf.Lerp(cam.transform.position.z, MapManager.Instance.range / 2, timer);
                cam.transform.position = new Vector3(lerpX, lerpY, lerpZ);

                if (timer >= 1)
                    break;

                yield return null;
            }

            EnemyTurnStart?.Invoke();
        }

        private IEnumerator CamZoomOut()
        {
            float timer = 0;

            while (true)
            {
                timer += Time.deltaTime;

                float lerpX = Mathf.Lerp(cam.transform.position.x, originCamPos.x, timer);
                float lerpY = Mathf.Lerp(cam.transform.position.y, originCamPos.y, timer);
                float lerpZ = Mathf.Lerp(cam.transform.position.z, originCamPos.z, timer);
                cam.transform.position = new Vector3(lerpX, lerpY, lerpZ);

                if (timer >= 1)
                    break;

                yield return null;
            }
            cam.Follow = PlayerManager.Instance.Player.transform;
        }

        public void EndBossRound()
        {
            isBossRound = false;
        }
    }
}
