using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Pool/Manager")]
public class PoolManagerSO : ScriptableObject
{
    public event Action CompletedInitEvent;
    public List<PoolingItemSO> poolingItemList = new();
    private Dictionary<string, Pool> _pools;
    [SerializeField] private Transform _rootTrm;

    public void InitializePool(Transform root)
    {
        _rootTrm = root;
        _pools = new Dictionary<string, Pool>();

        foreach (var item in poolingItemList)
        {
            var handle = item.prefab;
            IPoolable poolable = handle.GetComponent<IPoolable>();

            var pool = new Pool(poolable, _rootTrm, item.initCount);
            _pools.Add(item.poolType.typeName, pool);
        }

        CompletedInitEvent?.Invoke();
    }

    public IPoolable Pop(PoolTypeSO type)
    {
        if (_pools.TryGetValue(type.typeName, out Pool pool))
        {
            return pool.Pop();
        }

        return null;
    }

    //public void ReleasePoolAsset()
    //{
    //    foreach (var item in poolingItemList)
    //    {
    //        if (item.prefab)
    //            Destroy(item.prefab);
    //    }
    //}

    public void Push(IPoolable item)
    {
        if (_pools.TryGetValue(item.PoolType.typeName, out Pool pool))
        {
            pool.Push(item);
        }
    }
}