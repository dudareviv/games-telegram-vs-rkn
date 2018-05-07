using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObjectsPool
{
    public string Name;

    public GameObject ItemPrefab;

    public List<GameObject> Items = new List<GameObject>();

    public UnityEvent OnSpawnEvent = new UnityEvent();
    public UnityEvent OnDesctroyEvent = new UnityEvent();

    public void SpawnItem(Vector3 position, int limit = 0)
    {
        OnSpawnEvent.Invoke();

        var item = Items.Find(IsActive);

        if (item) {
            item.transform.position = position;
            item.SetActive(true);
        } else if (Items.Count < limit && limit > 0 || limit == 0) {
            item = GameObject.Instantiate(ItemPrefab, position, Quaternion.identity);
            Items.Add(item);
        }
    }

    private static bool IsActive(GameObject item)
    {
        return !item.activeInHierarchy;
    }

    public void DestroyItem(GameObject item, bool invokeDestroyEvent = true)
    {
        item.SetActive(false);

        if (invokeDestroyEvent)
            OnDesctroyEvent.Invoke();
    }

    public void Reset(Predicate<GameObject> match)
    {
        var items = Items.FindAll(match);

        foreach (var item in items) {
            DestroyItem(item, false);
        }
    }

    public void Reset()
    {
        foreach (var item in Items) {
            DestroyItem(item, false);
        }
    }
}