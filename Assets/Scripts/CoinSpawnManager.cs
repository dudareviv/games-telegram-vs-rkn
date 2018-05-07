using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : Singleton<CoinSpawnManager>
{
    public Transform PlayerTransform;

    public int CoinPoolLimit = 30;

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
            SpawnCoins();
            yield return new WaitForSeconds(SpawnCooldown * Random.Range(0.9f, 1.1f));
        }
    }

    private void SpawnCoins()
    {
        GameObjectsPoolsManager.Instance.Reset("Coin", IsCoinFarAway);

        for (int i = 0; i < Random.Range(SpawnCountMin, SpawnCountMax); i++) {
            var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var position = (Vector2) PlayerTransform.position;
            position = position + direction.normalized * Random.Range(SpawnRadiusMin, SpawnRadiusMax);

            GameObjectsPoolsManager.Instance.Spawn("Coin", position, CoinPoolLimit);
        }
    }

    private static bool IsCoinFree(GameObject coin)
    {
        return !coin.activeInHierarchy;
    }

    private bool IsCoinFarAway(GameObject coin)
    {
        return Vector2.Distance(PlayerTransform.position, coin.transform.position) > SpawnRadiusMax;
    }
}