using UnityEngine;

namespace KHJ
{
    public class Test : MonoBehaviour, IPoolable
    {
        public PoolTypeSO PoolType {get; set;}

        public GameObject GameObject => gameObject;

        public void ResetItem()
        {

        }

        public void SetUpPool(Pool pool)
        {

        }
    }
}
