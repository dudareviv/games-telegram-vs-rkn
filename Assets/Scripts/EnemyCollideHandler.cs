using UnityEngine;

public class EnemyCollideHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            GameObjectsPoolsManager.Instance.DestroyItem("Enemy", gameObject);
            GameObjectsPoolsManager.Instance.Spawn("FireExplosion", transform.position);
        }
    }
}