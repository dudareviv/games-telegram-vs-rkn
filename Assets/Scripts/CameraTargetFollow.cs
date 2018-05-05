using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFollow : MonoBehaviour
{


    public Transform Target;
    public Vector3 Offset;

    [Range(0.1f, 100f)]
    public float Scale = 1f;

    public float MaxAngle = 15f;
    public float MaxOffset = 1f;

    [Range(0.001f, 0.5f)]
    public float DecreaseTraumaSpeed = 0.02f;

    [Range(0f, 1f)]
    public float Trauma = 0f;

    public float Shake = 0f;

    public int Seed = 0;

    private void Awake()
    {
        Seed = Random.Range(0, 9999);
    }

    private void FixedUpdate()
    {
        if (Trauma > 0) {
            Trauma -= DecreaseTraumaSpeed * Time.fixedDeltaTime;

            if (Trauma < 0)
                Trauma = 0;

            Shake = Trauma * Trauma * Trauma;
        }
    }

    private void Update()
    {
        var cameraPosition = GetPosition();
        var cameraRotation = transform.eulerAngles;

        if (Trauma > 0) {
            var angle = MaxAngle * GetPerlineShake(0);
            var xOffset = MaxOffset * GetPerlineShake(1);
            var yOffset = MaxOffset * GetPerlineShake(2);

            cameraPosition += new Vector3(xOffset, yOffset);

            cameraRotation.z = 0 + angle;
        }

        transform.position = cameraPosition;
        transform.eulerAngles = cameraRotation;
    }

    private float GetPerlineShake(int index)
    {
        return Shake * (2f * Mathf.PerlinNoise(Time.time * Scale, Seed + index) - 1f);
    }

    private void OnValidate()
    {
        transform.position = GetPosition();
    }

    private Vector3 GetPosition()
    {
        return Target.position + Offset;
    }

    public void AddTrauma(float value)
    {
        Trauma += value;

        if (Trauma > 1)
            Trauma = 1;
    }
}