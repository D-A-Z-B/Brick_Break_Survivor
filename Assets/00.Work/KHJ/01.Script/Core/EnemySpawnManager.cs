using BBS.Players;
using UnityEngine;

namespace KHJ.Core
{
    public class EnemySpawnManager : MonoBehaviour
    {
        private MapManager mapManager => MapManager.Instance;

        [SerializeField] private Transform player;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnRadiusMin = 5f;
        [SerializeField] private float spawnRadiusMax = 20f;

        [SerializeField] private int spawnCount;

        private void Awake()
        {
            mapManager.OnCompleteSpawnMapEvent += HandleSpawnEnemy;
        }

        private void HandleSpawnEnemy()
        {
            int spawnedEnemies = 0;

            while (spawnedEnemies < spawnCount)
            {
                if (TrySpawnEnemy())
                {
                    spawnedEnemies++;
                }
            }
        }

        private bool TrySpawnEnemy()
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();

            float distanceToPlayer = Vector3.Distance(spawnPosition, player.position);

            float normalizedDistance = (distanceToPlayer - spawnRadiusMin) / (spawnRadiusMax - spawnRadiusMin);
            normalizedDistance = Mathf.Clamp01(normalizedDistance);

            float spawnChance = Mathf.Pow(1f - normalizedDistance, 4);

            if (Random.value <= spawnChance && MapCondition(spawnPosition))
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                return true;
            }

            return false;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(spawnRadiusMin, spawnRadiusMax);

            Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y) * randomDistance;

            randomPosition += player.position;

            return new Vector3((int)randomPosition.x, 0, (int)randomPosition.z); 
        }

        private bool MapCondition(Vector3 pos)
        {
            return !mapManager.mapBoard.Contains(pos) &&
                pos.x >= 0 && pos.x <= 40 &&
              pos.z >= 0 && pos.z <= 40;
        }
    }
}
