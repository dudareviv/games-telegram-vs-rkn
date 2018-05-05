using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Motor), typeof(Rotator))]
public class EnemyControl : MonoBehaviour
{
    public Transform PlayerTransform;

    private Motor _motor;
    private Rotator _rotator;

    private Vector2 directionToPlayer = new Vector2();

    private void Awake()
    {
        PlayerTransform = GameManager.Instance.Player.transform;
        _motor = GetComponent<Motor>();
        _rotator = GetComponent<Rotator>();
    }

    private void FixedUpdate()
    {
        directionToPlayer = PlayerTransform.position - transform.position;
    }

    private void Update()
    {
        _motor.Move();
        _rotator.RotateAt(directionToPlayer);
    }
}