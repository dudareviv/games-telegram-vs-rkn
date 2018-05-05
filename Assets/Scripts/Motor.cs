using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
    public float Speed = 1f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
//        var currentPosition = transform.position;
//        var targetPosition = currentPosition + transform.localRotation * Vector2.up * Speed;
//        var delta = Speed * Time.deltaTime;
//
//        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, delta);

        _rigidbody.velocity = transform.localRotation * Vector2.up * Speed ;
    }
}