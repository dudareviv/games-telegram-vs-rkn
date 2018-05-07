using UnityEngine;
using UnityEngine.Events;

public class PlayerCollideHandler : MonoBehaviour
{
    public UnityEvent OnEnemyHit = new UnityEvent();
    public UnityEvent OnCoinHit = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            OnEnemyHit.Invoke();

            EnemySpawnManager.DestroyEnemy(other.gameObject);
            ExplosionsSpawnManager.Instance.Spawn(other.gameObject.transform.position);

            HealthManager.Instance.GetDamage(1);
        }

        if (other.CompareTag("Coin")) {
            OnCoinHit.Invoke();

            ScoreManager.Instance.CollectCoin();
            CoinSpawnManager.DestroyCoin(other.gameObject);
        }
    }

}