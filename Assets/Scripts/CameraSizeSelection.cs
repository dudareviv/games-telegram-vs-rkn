using System.Linq;
using UnityEngine;

public class CameraSizeSelection : MonoBehaviour
{
    public Camera TargetCamera;
    public Transform TargetTransform;

    [Range(0.01f, 1f)]
    public float Speed = .5f;

    public float RadiusMin = 5f;
    public float RadiusMax = 8f;

    [Range(0.5f, 2f)]
    public float FurtherObjectRadius = .75f;

    [SerializeField]
    private float TargetSize;

    private EnemySpawnManager _enemyManager;

    private void Awake()
    {
        _enemyManager = EnemySpawnManager.Instance;
    }

    private bool FindCriterea(GameObject x)
    {
        return x.activeInHierarchy;
    }

    private float OrderCriterea(GameObject x)
    {
        return Vector2.Distance(TargetTransform.position, x.transform.position);
    }

    private void FixedUpdate()
    {
        var futher = _enemyManager.EnemyPool
            .Where(FindCriterea)
            .OrderByDescending(OrderCriterea)
            .FirstOrDefault();

        if (futher == null)
            return;

        var radius = Vector2.Distance(TargetTransform.position, futher.transform.position) + FurtherObjectRadius;
        radius = Mathf.Clamp(radius, RadiusMin, RadiusMax);

        TargetSize = radius / TargetCamera.aspect;
    }

    private void Update()
    {
        TargetCamera.orthographicSize = Mathf.Lerp(TargetCamera.orthographicSize, TargetSize, Speed);
    }
}