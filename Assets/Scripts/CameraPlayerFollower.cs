using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
    public Transform CameraContainerTransform;
    public Transform PlayerTransform;

    public Vector3 Offset;
    public float DirectionDistanceOffset;

    private void Update()
    {
        CameraContainerTransform.position = GetPosition();
    }

    private void OnValidate()
    {
        CameraContainerTransform.position = GetPosition();
    }

    private Vector3 GetPosition()
    {
        return PlayerTransform.position + Offset + PlayerTransform.localRotation * Vector2.up * DirectionDistanceOffset;
    }
}