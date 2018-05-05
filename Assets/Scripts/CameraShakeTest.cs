using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTest : MonoBehaviour
{
    private CameraTargetFollow _cameraTargetFollow;

    private void Awake()
    {
        _cameraTargetFollow = GetComponent<CameraTargetFollow>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            _cameraTargetFollow.AddTrauma(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _cameraTargetFollow.AddTrauma(0.3f);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            _cameraTargetFollow.AddTrauma(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            _cameraTargetFollow.AddTrauma(1f);
        }
    }

}