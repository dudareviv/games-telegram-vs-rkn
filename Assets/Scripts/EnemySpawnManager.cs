using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    public Transform PlayerTransform;

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

            GameObjectsPoolsManager.Instance.Spawn("Enemy", position, EnemyPoolLimit);
        }
    }
}