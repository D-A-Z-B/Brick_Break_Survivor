using BBS;
using BBS.Enemies;
using BBS.Entities;
using BBS.Players;
using DG.Tweening;
using KHJ.Camera;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KHJ.Core
{
    public enum EntityType
    {
        Player,
        Enemy,
        Empty
    }

    public class MapManager : MonoSingleton<MapManager>
    {
        public event Action OnSpawnEnemiesEvent;

        private Player player => PlayerManager.Instance.Player;

        [SerializeField] private GameObject groundMapObj, wallObj, groundTextureObj;
        [SerializeField] private Transform mapParent;
        [field: SerializeField] public int range { get; private set; }
        [SerializeField] private float interval;
        [SerializeField] private float renderMoveSpeed;

        [field: SerializeField] public bool isEliteOrBoss { get; set; } = false;

        [Header("EliteAndBossSet")]
        [SerializeField] private int bossRange;

        private int[] dirX = new int[] { 0, 0, 1, -1, 1, 1, -1, -1 };
        private int[] dirY = new int[] { -1, 1, 0, 0, -1, 1, -1, 1 };

        private EntityType[,] mapBoardArr;
        private Enemy[,] enemyBoardArr;

        private void Awake()
        {
            TurnManager.Instance.BossEvent += HandleEliteSpawn;

            SetRange();
        }

        private void Start()
        {
            SpawnMap();
        }

        private void HandleEliteSpawn()
        {
            DestoyAll();
            isEliteOrBoss = true;
            SetRange();
            SpawnMap();
        }

        private void SetRange()
        {
            if (isEliteOrBoss)
                range = bossRange;

            mapBoardArr = new EntityType[range, range];
            enemyBoardArr = new Enemy[range, range];
        }

        private void SpawnMap()
        {
            Transform mapPar = Instantiate(mapParent, transform);
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    GameObject ground = Instantiate(groundMapObj, mapPar);
                    ground.transform.position = new Vector3(i, 0, j) * interval;
                    mapBoardArr[i, j] = EntityType.Empty;

                    GameObject groundTex = Instantiate(groundTextureObj, mapPar);
                    groundTex.transform.position = new Vector3(i, 0.5f, j) * interval;
                }
            }

            SetWall(mapPar);

            int half = (range - 1) / 2;
            if (!isEliteOrBoss)
            {
                mapBoardArr[half, half] = EntityType.Player;
                player.transform.position = new Vector3(half, 1, half);
            }
            else
            {
                mapBoardArr[half, half - 6] = EntityType.Player;
                player.transform.position = new Vector3(half, 1, half - 6);
            }

            OnSpawnEnemiesEvent?.Invoke();
        }

        private void SetWall(Transform mapPar)
        {
            int half = range / 2 + 1;
            Vector3 centerPoint = new Vector3((range - 1) / 2, 1f, (range - 1) / 2);

            Vector3[] directions = {
            new Vector3(0, 0, half),
            new Vector3(0, 0, -half),
            new Vector3(-half, 0, 0),
            new Vector3(half, 0, 0)
            };

            for (int i = 0; i < directions.Length; i++)
            {
                Vector3 spawnPosition = centerPoint + directions[i];
                Transform wall = Instantiate(wallObj, mapPar).transform;
                wall.DOScaleX(range + 2, 0);
                wall.SetPositionAndRotation(spawnPosition, Quaternion.LookRotation(directions[i]));
            }
        }

        public EntityType GetPos(Coord coord) => mapBoardArr[coord.x, coord.y];

        public void SetPos(Coord coord, EntityType entity, bool isElite = false)
        {
            mapBoardArr[coord.x, coord.y] = entity;
            if (isElite)
                SetEliteType(coord, entity);
        }

        public void MoveEntity(Enemy enemy, Coord moveCoord, EntityType entity, bool isElite = false)
        {
            Coord currentCoord = new Coord(enemy.transform.position);
            Debug.Log($"{currentCoord.x}, {currentCoord.y}");
            Debug.Log($"{enemy.transform.position}");

            if (isElite)
            {
                if (!MapCondition(currentCoord) || !MapCondition(moveCoord))
                {
                    EnemySpawnManager.Instance.EnemyCount();
                    print("¸ØÃç");
                    return;
                }

                print("°¡ÀÚ");
                SetPos(currentCoord, EntityType.Empty, true);
                SetPos(moveCoord, entity, true);
            }
            else
            {
                if ((!MapCondition(currentCoord) || !MapCondition(moveCoord)) || (mapBoardArr[moveCoord.x, moveCoord.y] != EntityType.Empty))
                {
                    EnemySpawnManager.Instance.EnemyCount();
                    return;
                }
                SetPos(currentCoord, EntityType.Empty);
                SetPos(moveCoord, entity);
            }

            MoveRenderEnemy(enemy, moveCoord);
        }

        public void SetEnemyBoard(Coord coord, Enemy enemy)
        {
            enemyBoardArr[coord.x, coord.y] = enemy;
        }

        private void MoveRenderEnemy(Enemy enemy, Coord moveCoord)
        {
            Enemy moveEnemy = enemy;
            moveEnemy.DoMoveEnemy(moveCoord, renderMoveSpeed, moveEnemy.data.ease, moveEnemy is AssassinEnemy);

            SetEnemyBoard(new Coord(moveEnemy.transform.position), null);
            SetEnemyBoard(moveCoord, moveEnemy);
        }

        private bool MapCondition(Coord pos, bool isElite = false)
        {
            return !isElite?pos.x >= 0 && pos.x < range &&
              pos.y >= 0 && pos.y < range:
            pos.x >= 1 && pos.x < range - 1 &&
            pos.y >= 1 && pos.y < range - 1;
        }

        public Enemy GetEnemyInArr(int x, int y)
        {
            return enemyBoardArr[x, y];
        }

        private void SetEliteType(Coord coord, EntityType entity)
        {
            for (int i = 0; i < dirX.Length; i++)
            {
                int newX = coord.x + dirX[i];
                int newY = coord.y + dirY[i];

                if (MapCondition(new Coord(newX, newY), true))
                {
                    mapBoardArr[newX, newY] = entity;
                }
            }
        }


        public void DestroyEntity(Coord coord, Entity entity, bool isDestroyObj = false)
        {
            mapBoardArr[coord.x, coord.y] = EntityType.Empty;
            enemyBoardArr[coord.x, coord.y] = null;

            if (!isDestroyObj && entity is Player) return;

            EnemySpawnManager.Instance.enemyList.Remove(entity as Enemy);
            Destroy(entity.gameObject);
        }

        public void DestoyAll()
        {
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    mapBoardArr[i, j] = EntityType.Empty;
                    enemyBoardArr[i, j] = null;
                }
            }

            EnemySpawnManager.Instance.enemyList.ForEach(x => Destroy(x.gameObject));
            EnemySpawnManager.Instance.enemyList.Clear();

            Destroy(transform.GetChild(0).gameObject);
        }

        private Enemy FindEnemy(Enemy enemy)
        {
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    if (enemy == enemyBoardArr[i, j])
                        return enemy;
                }
            }
            return null;
        }
    }
}
