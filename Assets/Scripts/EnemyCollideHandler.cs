using UnityEngine;

public class EnemyCollideHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            EnemySpawnManager.DestroyEnemy(gameObject);
            ExplosionsSpawnManager.Instance.Spawn(transform.position);
        }
    }
}