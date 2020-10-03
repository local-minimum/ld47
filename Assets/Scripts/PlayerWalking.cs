﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    [SerializeField]
    AnimationCurve stepHeight;
    [SerializeField, Range(0, 1)]
    float stepLength;
    [SerializeField, Range(0, 4)]
    float stepDuration;

    [SerializeField] Camera eyeCamera;
    Vector3 cameraOrigin;

    float walkSpeed;
    float turnSpeed;

    [SerializeField, Range(0, 4)]
    float turn90Duration;

    void Start()
    {
        cameraOrigin = eyeCamera.transform.localPosition;
        StartCoroutine(Step());
    }

    // Update is called once per frame
    void Update()
    {
        walkSpeed = Input.GetAxis("Vertical");
        turnSpeed = Input.GetAxis("Horizontal");
        
    }

    IEnumerator<WaitForSeconds> Step()
    {
        while (true) {
            if (Mathf.Abs(walkSpeed) > 0.1f) {
                float duration = 0f;
                float stepSpeed = Mathf.Abs(walkSpeed);
                while (duration < stepDuration)
                {
                    eyeCamera.transform.localPosition = cameraOrigin + transform.up * stepHeight.Evaluate(duration / stepDuration);
                    yield return new WaitForSeconds(0.02f);
                    stepSpeed = Mathf.Max(stepSpeed, Mathf.Abs(walkSpeed));
                    duration += stepSpeed * 0.02f;
                    transform.position += transform.forward * walkSpeed * stepLength * 0.02f;
                }
                eyeCamera.transform.localPosition = cameraOrigin + transform.up * stepHeight.Evaluate(1f);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}