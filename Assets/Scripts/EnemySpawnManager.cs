using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    public Transform PlayerTransform;

    public GameObject EnemyPrefab;

    public List<GameObject> EnemyPool;
    public int EnemyPoolLimit = 15;

    public float SpawnRadiusMin = 3f;
    public float SpawnRadiusMax = 3f;

    public int SpawnCountMin = 1;
    public int SpawnCountMax = 5;

    public float SpawnCooldown = 2f;

    private void Awake()
    {
        PlayerTransform = GameManager.Instance.Player.transform;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true) {
            SpawnEnemies();
            yield return new WaitForSeconds(SpawnCooldown * Random.Range(0.9f, 1.1f));
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < Random.Range(SpawnCountMin, SpawnCountMax); i++) {
            var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var position = (Vector2) PlayerTransform.position;
            position = position + direction.normalized * Random.Range(SpawnRadiusMin, SpawnRadiusMax);

            InstantiateEnemy(position);
        }
    }

    private static bool IsEnemyFree(GameObject enemy)
    {
        return !enemy.activeInHierarchy;
    }

    private void InstantiateEnemy(Vector2 position)
    {
        var enemy = EnemyPool.Find(IsEnemyFree);

        if (enemy) {
            enemy.transform.position = position;
            enemy.SetActive(true);
        } else if (EnemyPool.Count < EnemyPoolLimit) {
            enemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
            EnemyPool.Add(enemy);
        }
    }

    public static void DestroyEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    public void Reset()
    {
        foreach (var enemy in EnemyPool) {
            DestroyEnemy(enemy);
        }
    }

}