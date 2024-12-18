using BBS;
using BBS.Enemies;
using DG.Tweening;
using System;
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

        [SerializeField] private GameObject groundMapObj, wallObj;
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
            if (isEliteOrBoss)
                range = bossRange;

            mapBoardArr = new EntityType[range, range];
            enemyBoardArr = new Enemy[range, range];
        }

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
                    GameObject ground = Instantiate(groundMapObj, transform);
                    ground.transform.position = new Vector3(i, 0, j) * interval;
                    mapBoardArr[i, j] = EntityType.Empty;
                }
            }

            SetWall();

            if (!isEliteOrBoss)
                mapBoardArr[(range - 1) / 1, (range - 1) / 2] = EntityType.Player;
            else
                mapBoardArr[(range - 1) / 1, (range - 1) / 2 - 6] = EntityType.Player;

            OnSpawnEnemiesEvent?.Invoke();
        }

        private void SetWall()
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
                Transform wall = Instantiate(wallObj, transform).transform;
                wall.DOScaleX(range, 0);
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

        public void MoveEntity(Coord currentCoord, Coord moveCoord, EntityType entity, bool isElite = false)
        {
            if (isElite)
            {
                if (!MapCondition(currentCoord, true) || !MapCondition(moveCoord, true)) return;
                SetPos(currentCoord, EntityType.Empty, true);
                SetPos(moveCoord, entity, true);
            }
            else
            {
                if (!MapCondition(currentCoord) || !MapCondition(moveCoord)) return;
                if (mapBoardArr[moveCoord.x, moveCoord.y] != EntityType.Empty) return;
                SetPos(currentCoord, EntityType.Empty);
                SetPos(moveCoord, entity);
            }

            MoveRenderEnemy(currentCoord, moveCoord);
        }

        public void SetEnemyBoard(Coord coord, Enemy enemy)
        {
            enemyBoardArr[coord.x, coord.y] = enemy;
        }

        private void MoveRenderEnemy(Coord currentCoord, Coord moveCoord)
        {
            Enemy moveEnemy = enemyBoardArr[currentCoord.x, currentCoord.y];

            moveEnemy.DoMoveEnemy(moveCoord, renderMoveSpeed, moveEnemy is AssassinEnemy);

            SetEnemyBoard(currentCoord, null);
            SetEnemyBoard(moveCoord, moveEnemy);
        }

        private bool MapCondition(Coord pos, bool isElite = false)
        {
            return !isElite ? pos.x >= 0 && pos.x < 40 &&
              pos.y >= 0 && pos.y < 40 : pos.x >= 1 && pos.x < 39 &&
              pos.y >= 1 && pos.y < 39;
        }

        public Enemy GetEnemyInArr(int x, int y)
        {
            return enemyBoardArr[x, y];
        }

        private void SetEliteType(Coord coord, EntityType entity)
        {
            for (int i = 0; i < dirX.Length; i++)
            {
                mapBoardArr[coord.x + dirX[i], coord.y + dirY[i]] = entity;
            }
        }

        public void DestroyEntity(Coord coord)
        {
            mapBoardArr[coord.x, coord.y] = EntityType.Empty;
            enemyBoardArr[coord.x, coord.y] = null;
        }
    }
}
