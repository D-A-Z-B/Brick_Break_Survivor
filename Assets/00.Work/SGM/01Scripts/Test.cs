using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace BBS
{
    public class Test : MonoBehaviour
    {
        public List<Transform> spawnList;

        private void Start()
        {
            spawnList.ForEach((trm) =>
            {
                LevelManager.Instance.CreateExp(trm.position);
            });
        }
    }
}
