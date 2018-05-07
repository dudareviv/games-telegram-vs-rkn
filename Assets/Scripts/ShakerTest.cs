using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerTest : MonoBehaviour
{
    private Shaker _shaker;

    private void Awake()
    {
        _shaker = GetComponent<Shaker>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            _shaker.AddTrauma(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _shaker.AddTrauma(0.3f);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            _shaker.AddTrauma(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            _shaker.AddTrauma(1f);
        }
    }

}