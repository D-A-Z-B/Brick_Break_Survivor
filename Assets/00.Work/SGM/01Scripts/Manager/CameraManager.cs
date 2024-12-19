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

        private float originYPos;

        private void Start()
        {
            originYPos = cam.transform.position.y;
        }

        public void StartZoomOut()
        {
            StartCoroutine(ZoomOut());
        }

        private IEnumerator ZoomOut()
        {
            float timer = 0;

            cam.Follow = null;
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

        public void StartZoomIn()
        {
            StartCoroutine(ZoomIn());
        }

        private IEnumerator ZoomIn()
        {
            yield return new WaitForSeconds(2f);

            float timer = 0;

            Transform playerTrm = PlayerManager.Instance.Player.transform;
            while (true)
            {
                timer += Time.deltaTime;

                float lerpX = Mathf.Lerp(cam.transform.position.x, playerTrm.position.x, timer);
                float lerpY = Mathf.Lerp(cam.transform.position.y, originYPos, timer);
                float lerpZ = Mathf.Lerp(cam.transform.position.z, playerTrm.position.z, timer);
                cam.transform.position = new Vector3(lerpX, lerpY, lerpZ);

                if (timer >= 1)
                    break;

                yield return null;
            }
            cam.Follow = playerTrm;
        }
    }
}
