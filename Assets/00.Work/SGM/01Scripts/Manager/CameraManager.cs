using KHJ.Core;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace BBS
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineCamera cam;
        [SerializeField] private float ZoomInYPos = 32;

        Vector3 originCamPos;

        public void StartZoomIn()
        {
            StartCoroutine(ZoomIn());
        }

        private IEnumerator ZoomIn()
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

            TurnManager.Instance.EnemyTurnStart?.Invoke();
        }

        public void StartZoomOut()
        {
            StartCoroutine(ZoomOut());
        }

        private IEnumerator ZoomOut()
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
    }
}
