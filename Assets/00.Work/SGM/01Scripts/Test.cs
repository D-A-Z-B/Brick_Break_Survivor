using KHJ.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BBS
{
    public class Test : MonoSingleton<Test>
    {
        public List<Transform> spawnList;

        public Action OnChangeTurn;

        //private void Start()
        //{
        //    spawnList.ForEach((trm) =>
        //    {
        //        LevelManager.Instance.CreateExp(trm.position);
        //    });
        //}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TurnManager.Instance.ChangeTurn(TurnType.EnemyTurn);
            }
        }
    }
}
