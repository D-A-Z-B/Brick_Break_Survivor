using UnityEngine;

namespace KHJ
{
    public class MapManager : MonoBehaviour
    {
        [Header("Pool")]
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolTypeSO groundType;

        [Space]
        [SerializeField] private GameObject groundMapObj;
        [SerializeField] private int range;
        [SerializeField] private float interval; 

        private void Start()
        {
            SpawnMap();
        }

        private void SpawnMap()
        {
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    Ground ground = poolManager.Pop(groundType) as Ground;
                    ground.transform.position = new Vector3(i, 0, j) * interval;
                }
            }
        }
    }
}
