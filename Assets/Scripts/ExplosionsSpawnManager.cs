using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionsSpawnManager : Singleton<ExplosionsSpawnManager>
{
    public GameObject ItemPrefab;

    public List<GameObject> ItemsPool = new List<GameObject>();

    public UnityEvent OnSpawnEvent = new UnityEvent();

    public void Spawn(Vector3 position)
    {
        OnSpawnEvent.Invoke();

        var item = ItemsPool.Find(IsActive);

        if (item) {
            item.transform.position = position;
            item.SetActive(true);
        } else {
            item = Instantiate(ItemPrefab, position, Quaternion.identity);
            ItemsPool.Add(item);
        }
    }

    private static bool IsActive(GameObject item)
    {
        return !item.activeInHierarchy;
    }

    public void DestroyItem(GameObject item)
    {
        item.SetActive(false);
    }

    public void Reset()
    {
        foreach (var item in ItemsPool) {
            DestroyItem(item);
        }
    }
}