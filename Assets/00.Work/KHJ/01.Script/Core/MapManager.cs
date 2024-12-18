using System;
using System.Collections.Generic;
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

        public EntityType[,] mapBoardArr { get; private set; } = new EntityType[40, 40];

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

            OnCompleteSpawnMapEvent?.Invoke();
        }

        public EntityType GetPos(Coord coord) => mapBoardArr[coord.x, coord.y];

        public void SetPos(Coord coord, EntityType entity) => mapBoardArr[coord.x, coord.y] = entity;

        public void MoveEntity(Coord currentCoord, Coord moveCoord, EntityType entity)
        {
            mapBoardArr[currentCoord.x, currentCoord.y] = EntityType.Empty;
            mapBoardArr[moveCoord.x, moveCoord.y] = entity;
        }
    }
}
