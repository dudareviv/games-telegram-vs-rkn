using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Motor), typeof(Rotator))]
public class PlayerControl : MonoBehaviour
{
    private Motor _motor;
    private Rotator _rotator;

    private int currentTouchFingerId;
    private bool isTouching;

    [SerializeField]
    private RotateDirection _rotateDirection;

    private void Awake()
    {
        _motor = GetComponent<Motor>();
        _rotator = GetComponent<Rotator>();
    }

    private void Update()
    {
        foreach (var touch in Input.touches) {
            HandleTouch(touch.fingerId, touch.position, touch.phase);
        }

        if (Input.touchCount == 0) {
            if (Input.GetMouseButtonDown(0)) {
                HandleTouch(10, Input.mousePosition, TouchPhase.Began);
            }
            if (Input.GetMouseButton(0)) {
                HandleTouch(10, Input.mousePosition, TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0)) {
                HandleTouch(10, Input.mousePosition, TouchPhase.Ended);
            }
        }

        _motor.Move();

        if (!isTouching) {
            float axis = Input.GetAxis("Horizontal");
            if (Math.Abs(axis) > 0.05f) {
                _rotateDirection = axis > 0 ? RotateDirection.CLOCKWISE : RotateDirection.COUNTER_CLOCKWISE;
            } else {
                _rotateDirection = RotateDirection.NONE;
            }
        }

        _rotator.Rotate(_rotateDirection);
    }

    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        if (isTouching && touchFingerId != currentTouchFingerId)
            return;

        touchPosition = Camera.main.ScreenToViewportPoint(touchPosition);

        switch (touchPhase) {
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                _rotateDirection = RotateDirection.NONE;
                isTouching = false;
                break;

            case TouchPhase.Began:
                currentTouchFingerId = touchFingerId;
                _rotateDirection = touchPosition.x > 0.5f
                    ? RotateDirection.CLOCKWISE
                    : RotateDirection.COUNTER_CLOCKWISE;
                isTouching = true;
                break;

            case TouchPhase.Moved:
                _rotateDirection = touchPosition.x > 0.5f
                    ? RotateDirection.CLOCKWISE
                    : RotateDirection.COUNTER_CLOCKWISE;
                break;

            case TouchPhase.Stationary:
                break;

            default:
                throw new ArgumentOutOfRangeException("touchPhase", touchPhase, null);
        }
    }
}