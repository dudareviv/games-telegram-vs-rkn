using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthDisplay : MonoBehaviour
{
    public GameObject HealthItemPrefab;

    private List<GameObject> HealthItemsPool = new List<GameObject>();

    private void Awake()
    {
        HealthItemsPool = new List<GameObject>();

        for (int i = 0; i < HealthManager.Instance.CurrentHealth; i++) {
            AddHealth();
        }
    }

    public void OnHealthUpdate(int value)
    {
        for (int i = 0; i < HealthItemsPool.Count; i++) {
            HealthItemsPool[i].SetActive(i < value);
        }
    }

    private void AddHealth()
    {
        var healthGameObject = Instantiate(HealthItemPrefab);
        healthGameObject.transform.SetParent(gameObject.transform);
        healthGameObject.transform.localScale = Vector3.one;

        HealthItemsPool.Add(healthGameObject);
    }
}