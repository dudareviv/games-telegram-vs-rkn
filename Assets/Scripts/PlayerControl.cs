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

    private List<int> _touches = new List<int>();

    [SerializeField]
    private RotateDirection _rotateDirection;

    private void Awake()
    {
        _motor = GetComponent<Motor>();
        _rotator = GetComponent<Rotator>();
        _touches = new List<int>();
    }

    private void Update()
    {
#if (UNITY_ANDROID || UNITY_IOS)
        foreach (var touch in Input.touches) {
            HandleTouch(touch.position, touch.phase);
        }
#endif

#if (UNITY_EDITOR || UNITY_STANDALONE)
        if (Input.touchCount == 0) {
            if (Input.GetMouseButtonDown(0)) {
                HandleTouch(Input.mousePosition, TouchPhase.Began);
            }
            if (Input.GetMouseButton(0)) {
                HandleTouch(Input.mousePosition, TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0)) {
                HandleTouch(Input.mousePosition, TouchPhase.Ended);
            }
        }
#endif

        _motor.Move();

#if (UNITY_EDITOR || UNITY_STANDALONE)
        float axis = Input.GetAxis("Horizontal");
        if (Math.Abs(axis) > 0.05f) {
            _rotateDirection = axis > 0 ? RotateDirection.CLOCKWISE : RotateDirection.COUNTER_CLOCKWISE;
        } else {
            _rotateDirection = RotateDirection.NONE;
        }
#endif

        _rotator.Rotate(_rotateDirection);
    }

    private void HandleTouch(Vector3 touchPosition, TouchPhase touchPhase)
    {
        var screenTouchPosition = Camera.main.ScreenToViewportPoint(touchPosition);

        switch (touchPhase) {

            case TouchPhase.Began:
                _rotateDirection = screenTouchPosition.x > 0.5f
                    ? RotateDirection.CLOCKWISE
                    : RotateDirection.COUNTER_CLOCKWISE;
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                _rotateDirection = screenTouchPosition.x > 0.5f
                    ? RotateDirection.CLOCKWISE
                    : RotateDirection.COUNTER_CLOCKWISE;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                _rotateDirection = RotateDirection.NONE;
                break;

            default:
                throw new ArgumentOutOfRangeException("touchPhase", touchPhase, null);
        }
    }
}