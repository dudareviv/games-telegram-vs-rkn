using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPoolsManager : Singleton<GameObjectsPoolsManager>
{
    public GameObjectsPool[] GameObjectsPools;

    private Dictionary<string, GameObjectsPool> _pools;

    private void Awake()
    {
        InitPools();
    }

    private void OnValidate()
    {
        InitPools();
    }

    private void InitPools()
    {
        _pools = new Dictionary<string, GameObjectsPool>();

        foreach (var gameObjectsPool in GameObjectsPools) {
            if (_pools.ContainsKey(gameObjectsPool.Name))
                Debug.LogWarning("Name collision detected: " + gameObjectsPool.Name);

            _pools.Add(gameObjectsPool.Name, gameObjectsPool);
        }
    }

    public GameObjectsPool GetPool(string key)
    {
        if (!_pools.ContainsKey(key)) {
            Debug.LogError("Pool " + key + " is not found.");
            return null;
        }

        return _pools[key];
    }
    
    public void Spawn(string key, Vector3 position, int limit = 0)
    {
        if (!_pools.ContainsKey(key)) {
            Debug.LogError("Pool " + key + " is not found.");
            return;
        }

        _pools[key].SpawnItem(position, limit);
    }

    public void DestroyItem(string key, GameObject item, bool invokeDestroyEvent = true)
    {
        if (!_pools.ContainsKey(key)) {
            Debug.LogError("Pool " + key + " is not found.");
            return;
        }

        _pools[key].DestroyItem(item, invokeDestroyEvent);
    }

    public void Reset(string key)
    {
        if (!_pools.ContainsKey(key)) {
            Debug.LogError("Pool " + key + " is not found.");
            return;
        }

        _pools[key].Reset();
    }

    public void Reset(string key, Predicate<GameObject> match)
    {
        if (!_pools.ContainsKey(key)) {
            Debug.LogError("Pool " + key + " is not found.");
            return;
        }

        _pools[key].Reset(match);
    }

    public void Reset()
    {
        foreach (KeyValuePair<string, GameObjectsPool> gameObjectsPoolKeyValuePair in _pools) {
            Reset(gameObjectsPoolKeyValuePair.Key);
        }
    }
}