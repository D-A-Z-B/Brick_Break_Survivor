using UnityEngine;

namespace BBS.Players {
    [RequireComponent(typeof(LineRenderer))]
    public class Arrow : MonoBehaviour {
        [SerializeField] private Transform playerTrm;
        [SerializeField] private Transform lineEndPoint;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private float minSize;
        [SerializeField] private float maxSize;
        [SerializeField] private float minOffset; // 최소 거리
        [SerializeField] private float maxOffset; // 최대 거리
        [SerializeField] private float triggerDistance; // 마우스와 플레이어 사이 최대 거리 기준
        private float currentOffset;
        private float currentSize;

        private Plane plane;
        private LineRenderer lineRenderer;

        private void Start() {
            plane = new Plane(Vector3.up, new Vector3(0, playerTrm.position.y, 0));

            // LineRenderer 설정
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.useWorldSpace = true;

            gameObject.SetActive(false);
        }

        private void Update() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float distance)) {
                Vector3 mouseWorldPos = ray.GetPoint(distance);
                float mouseDistance = Vector3.Distance(playerTrm.position, mouseWorldPos);

                if (mouseDistance >= triggerDistance) {
                    currentOffset = maxOffset;
                    currentSize = maxSize;
                } else {
                    currentOffset = Mathf.Lerp(minOffset, maxOffset, mouseDistance / triggerDistance);
                    currentSize = Mathf.Lerp(minSize, maxSize, mouseDistance / triggerDistance);
                }

                Vector3 direction = (playerTrm.position - mouseWorldPos).normalized;

                transform.position = playerTrm.position + direction * currentOffset + Vector3.up;
                transform.localScale = new Vector3(currentSize, 1, 1);

                float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(90, 0, angle - 180);


                RaycastHit hit;
                if (Physics.Raycast(playerTrm.position, direction, out hit, 200f, obstacleLayer)) {
                    lineEndPoint.position = hit.point;
                } else {
                    lineEndPoint.position = playerTrm.position + direction * 200f;
                }
                lineRenderer.SetPosition(0, playerTrm.position);
                lineRenderer.SetPosition(1, lineEndPoint.position);  

            }
        }

        public Transform GetLineEndPoint() {
            return lineEndPoint;
        }
    }
}
