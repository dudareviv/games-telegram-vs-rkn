using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RotationSpeed = 1f;

    public void Rotate(RotateDirection rotateDirection)
    {
        if (rotateDirection == RotateDirection.NONE)
            return;

        var delta = RotationSpeed * Time.deltaTime;
        var angles = transform.localEulerAngles;
        var targetAngle = transform.localEulerAngles;

        switch (rotateDirection) {
            case RotateDirection.NONE:
                break;

            case RotateDirection.COUNTER_CLOCKWISE:
                targetAngle.z += RotationSpeed;
                break;

            case RotateDirection.CLOCKWISE:
                targetAngle.z -= RotationSpeed;
                break;

            default:
                throw new ArgumentOutOfRangeException("rotateDirection", rotateDirection, null);
        }

        angles.z = Mathf.MoveTowards(transform.localEulerAngles.z, targetAngle.z, delta);

        transform.localEulerAngles = angles;
    }

    public void RotateAt(Vector2 direction)
    {
        var delta = RotationSpeed * Time.deltaTime;
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        var targetQuaternion = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, delta);
    }
}