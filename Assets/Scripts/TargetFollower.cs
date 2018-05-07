using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    private void Update()
    {
        transform.position = GetPosition();
    }

    private void OnValidate()
    {
        transform.position = GetPosition();
    }

    private Vector3 GetPosition()
    {
        return Target.position + Offset;
    }
}