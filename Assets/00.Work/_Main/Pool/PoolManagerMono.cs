using System;
using UnityEngine;

public class PoolManagerMono : MonoBehaviour
{
    [SerializeField] private PoolManagerSO _poolManager;

    private void Awake()
    {
        Debug.Log(_poolManager);
        _poolManager.InitializePool(transform);
    }

    //private void OnDestroy()
    //{
    //    _poolManager.ReleasePoolAsset();
    //}
}