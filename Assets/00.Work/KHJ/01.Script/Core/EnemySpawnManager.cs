using BBS;
using BBS.Enemies;
using BBS.Players;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace KHJ.Core
{
    public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
    {
        private MapManager mapManager => MapManager.Instance;

        public List<Enemy> enemyList { get; private set; } = new List<Enemy>();
        private int currentEnemyCount = 0;

        private Player player
        {
            get
            {
                if (PlayerManager.Instance != null)
                {
                    return PlayerManager.Instance.Player;
                }
                return null;
            }
        }
        [SerializeField] private List<Enemy> enemyPrefab;
        [SerializeField] private int spawnRadiusMin;
        [SerializeField] private int spawnRadiusMax;
        [SerializeField] private int spawnCount;
        [SerializeField] private Transform spawnPointWithoutPlyer;

        [Header("EnemyRespawnInfo")]
        [SerializeField] private int howTurn;
        [SerializeField] private int prob;

        private void Awake()
        {
            mapManager.OnSpawnEnemiesEvent += HandleSpawnEnemy;
            TurnManager.Instance.TurnStartEvent += ReSpawnEnemy;
        }

        public void ReSpawnEnemy()
        {
            if (enemyList.Count <= 0)
            {
                HandleSpawnEnemy();
                return;
            }

            if (TurnManager.Instance.TurnCount % howTurn == 0)
            {
                int rand = Random.Range(1, 11);
                if (rand <= prob)
                    HandleSpawnEnemy();
            }
        }

        public void ReSpawnElite()
        {
            spawnCount = 7;
            HandleSpawnEnemy();
        }

        private void HandleSpawnEnemy()
        {
            if (mapManager.isEliteOrBoss)
                spawnCount = 1;

            int spawnedEnemies = 0;

            while (spawnedEnemies < spawnCount)
            {
                if (TrySpawnEnemy())
                {
                    spawnedEnemies++;
                }
            }

            TurnManager.Instance.modifyStatCount++;
        }

        private bool TrySpawnEnemy()
        {
            Vector3 spawnPosition;
            float spawnChance;
            if (!mapManager.isEliteOrBoss)
            {
                spawnPosition = GetRandomSpawnPosition();
                float distanceToPlayer;
                if (player == null)
                {
                    distanceToPlayer = Vector3.Distance(spawnPosition, spawnPointWithoutPlyer.position);
                }
                else
                {
                    distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);
                }

                float normalizedDistance = (distanceToPlayer - spawnRadiusMin) / (spawnRadiusMax - spawnRadiusMin);
                normalizedDistance = Mathf.Clamp01(normalizedDistance);

                spawnChance = Mathf.Pow(1f - normalizedDistance, 2);
            }
            else
            {
                spawnChance = 1;
                spawnPosition = new Vector3((mapManager.range - 1) / 2, 1, (mapManager.range - 1) / 2);
            }

            if (Random.value <= spawnChance && MapCondition(spawnPosition))
            {
                if (mapManager.GetPos(new Coord(spawnPosition)) != EntityType.Empty) return false;

                Enemy enemy = Instantiate(EnemyType(), spawnPosition, Quaternion.identity);
                mapManager.SetPos(new Coord(spawnPosition), EntityType.Enemy, mapManager.isEliteOrBoss);
                mapManager.SetEnemyBoard(new Coord(spawnPosition), enemy);
                enemyList.Add(enemy);
                mapManager.isEliteOrBoss = false;
                return true;
            }

            return false;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(spawnRadiusMin, spawnRadiusMax);

            Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y) * randomDistance;

            if (player == null)
            {
                randomPosition += spawnPointWithoutPlyer.position;
            }
            else
            {
                randomPosition += player.transform.position;
            }

            return new Vector3((int)randomPosition.x, 1, (int)randomPosition.z);
        }

        private Enemy EnemyType()
        {
            if (mapManager.isEliteOrBoss)
                return enemyPrefab[3];

            float randomValue = Random.Range(0f, 100f);

            if (randomValue < 50f)
                return enemyPrefab[0];
            else if (randomValue < 80f)
                return enemyPrefab[1];
            else
                return enemyPrefab[2];
        }

        private bool MapCondition(Vector3 pos)
        {
            return pos.x >= 0 && pos.x < mapManager.range &&
              pos.z >= 0 && pos.z < mapManager.range;
        }

        public void EnemyCount()
        {
            currentEnemyCount++;
            if (currentEnemyCount >= enemyList.Count)
            {
                CameraManager.Instance.StartZoomIn();
                currentEnemyCount = 0;
            }
        }
    }
}
