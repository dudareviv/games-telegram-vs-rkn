using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : Singleton<CoinSpawnManager>
{
    public Transform PlayerTransform;

    public GameObject CoinPrefab;
    public List<GameObject> CoinPool;
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
        var coins = CoinPool.FindAll(IsCoinFarAway);

        foreach (var coin in coins) {
            DestroyCoin(coin);
        }

        for (int i = 0; i < Random.Range(SpawnCountMin, SpawnCountMax); i++) {
            var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var position = (Vector2) PlayerTransform.position;
            position = position + direction.normalized * Random.Range(SpawnRadiusMin, SpawnRadiusMax);

            InstantiateCoin(position);
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

    private void InstantiateCoin(Vector2 position)
    {
        var coin = CoinPool.Find(IsCoinFree);

        if (coin) {
            coin.SetActive(true);
            coin.transform.position = position;
        } else if (CoinPool.Count < CoinPoolLimit) {
            coin = Instantiate(CoinPrefab, position, Quaternion.identity);
            CoinPool.Add(coin);
        }
    }

    public static void DestroyCoin(GameObject coin)
    {
        coin.SetActive(false);
    }

    public void Reset()
    {
        foreach (var coin in CoinPool) {
            DestroyCoin(coin);
        }
    }
}