using BBS.Enemies;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
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
        public event Action OnCompleteSpawnMapEvent;

        [SerializeField] private GameObject groundMapObj;
        [field: SerializeField] public int range { get; private set; }
        [SerializeField] private float interval;
        [SerializeField] private float renderMoveSpeed;

        [field: SerializeField] public bool isEliteOrBoss { get; private set; } = false;

        [Header("EliteAndBossSet")]
        [SerializeField] private int bossRange;

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
            mapBoardArr[range / 2, range / 2] = EntityType.Player;

            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    GameObject ground = Instantiate(groundMapObj, transform);
                    ground.transform.position = new Vector3(i, 0, j) * interval;
                    mapBoardArr[i, j] = EntityType.Empty;
                }
            }

            OnCompleteSpawnMapEvent?.Invoke();
        }

        public EntityType GetPos(Coord coord) => mapBoardArr[coord.x, coord.y];

        public void SetPos(Coord coord, EntityType entity) => mapBoardArr[coord.x, coord.y] = entity;

        public void MoveEntity(Coord currentCoord, Coord moveCoord, EntityType entity)
        {
            if (!MapCondition(currentCoord) || !MapCondition(moveCoord)) return;
            if (mapBoardArr[moveCoord.x, moveCoord.y] != EntityType.Empty) return;

            mapBoardArr[currentCoord.x, currentCoord.y] = EntityType.Empty;
            mapBoardArr[moveCoord.x, moveCoord.y] = entity;

            MoveRenderEnemy(currentCoord, moveCoord);
        }

        public void SetEnemyBoard(Coord coord, Enemy enemy)
        {
            enemyBoardArr[coord.x, coord.y] = enemy;
        }

        private void MoveRenderEnemy(Coord currentCoord, Coord moveCoord)
        {
            Enemy moveEnemy = enemyBoardArr[currentCoord.x, currentCoord.y];
            moveEnemy.transform.DOMove(new Vector3(moveCoord.x, 1, moveCoord.y), renderMoveSpeed).SetEase(Ease.Linear);

            SetEnemyBoard(currentCoord, null);
            SetEnemyBoard(moveCoord, moveEnemy);
        }

        private bool MapCondition(Coord pos)
        {
            return pos.x >= 0 && pos.x < 40 &&
              pos.y >= 0 && pos.y < 40;
        }
    }
}
