using BBS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KHJ.Core
{
    public class MapManager : MonoSingleton<MapManager>
    {
        public List<Vector3> mapBoard = new();
        public event Action OnCompleteSpawnMapEvent;

        [SerializeField] private GameObject groundMapObj;
        [field: SerializeField] public int range { get; private set; }
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
                    GameObject ground = Instantiate(groundMapObj, transform);
                    ground.transform.position = new Vector3(i, 0, j) * interval;
                    mapBoard.Add(ground.transform.position);
                }
            }

            OnCompleteSpawnMapEvent?.Invoke();
        }
    }
}
