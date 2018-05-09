using System.Linq;
using UnityEngine;

public class CameraSizeSelection : MonoBehaviour
{
    public Camera MainCamera;
    public Transform CameraContainerTransform;
    public Transform PlayerTransform;

    [Range(0.01f, 1f)]
    public float Speed = .5f;

    public float RadiusMin = 5f;
    public float RadiusMax = 8f;

    [Range(0.5f, 2f)]
    public float FurtherObjectRadius = .75f;

    [SerializeField]
    private float TargetSize;

    private float CameraAspect;

    private GameObjectsPool _enemyPool;

    private void Awake()
    {
        _enemyPool = GameObjectsPoolsManager.Instance.GetPool("Enemy");

        InitAspectAndSize();
    }

    private void OnValidate()
    {
        InitAspectAndSize();
    }

    private void InitAspectAndSize()
    {
        CameraAspect = MainCamera.aspect;

        if (CameraAspect > 1)
            CameraAspect = 1 / CameraAspect;

        TargetSize = RadiusMin / CameraAspect;
    }

    private bool FindCriterea(GameObject x)
    {
        return x.activeInHierarchy;
    }

    private float OrderCriterea(GameObject x)
    {
        return Vector2.Distance(CameraContainerTransform.position, x.transform.position);
    }

    private void FixedUpdate()
    {
        var futher = _enemyPool.Items
            .Where(FindCriterea)
            .OrderByDescending(OrderCriterea)
            .FirstOrDefault();

        if (futher == null)
            return;

        var radius = Vector2.Distance(CameraContainerTransform.position, futher.transform.position) +
                     FurtherObjectRadius;
        radius = Mathf.Clamp(radius, RadiusMin, RadiusMax);

        TargetSize = radius / CameraAspect;
    }

    private void Update()
    {
        MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, TargetSize, Speed);
    }
}