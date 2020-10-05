using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    [SerializeField]
    AnimationCurve stepHeight;
    [SerializeField, Range(0, 1.5f)]
    float stepLength;
    [SerializeField, Range(0, 4)]
    float stepDuration;

    [SerializeField] Camera eyeCamera;
    Vector3 cameraOrigin;

    float walkSpeed;
    float turnSpeed;

    [SerializeField, Range(0, 4)]
    float turn90Duration;

    Eyes eyes;

    [SerializeField]
    Transform phone;

    void Start()
    {
        cameraOrigin = eyeCamera.transform.localPosition;
        eyes = GetComponentInChildren<Eyes>();
        StartCoroutine(Step());
    }

    [SerializeField, Range(0, 10f)]
    float lateralLook = 2f;

    [SerializeField, Range(0, 10f)]
    float lookDistance = 5f;

    // Update is called once per frame
    void Update()
    {
        walkSpeed = Input.GetAxis("Vertical");
        turnSpeed = Input.GetAxis("Horizontal");
        float xLookFactor = Input.mousePosition.x / Screen.width - 0.5f;
        Vector3 lookAt = transform.position + transform.forward * lookDistance + xLookFactor * lateralLook * transform.right;
        eyes.LookAt(lookAt);
        phone.transform.LookAt(lookAt);
    }

    IEnumerator<WaitForSeconds> Step()
    {
        float factor = 0.02f;
        while (true) {
            if (Mathf.Abs(walkSpeed) > 0.1f) {
                float duration = 0f;
                float stepSpeed = Mathf.Abs(walkSpeed);
                while (duration < stepDuration)
                {
                    eyeCamera.transform.localPosition = cameraOrigin + transform.up * stepHeight.Evaluate(duration / stepDuration);
                    yield return new WaitForSeconds(factor);
                    stepSpeed = Mathf.Max(stepSpeed, Mathf.Abs(walkSpeed));
                    duration += stepSpeed * factor;
                    transform.position += transform.forward * walkSpeed * stepLength * factor;
                    transform.Rotate(transform.up, turnSpeed * factor * 90f / turn90Duration);
                }
                eyeCamera.transform.localPosition = cameraOrigin + transform.up * stepHeight.Evaluate(1f);
            }
            transform.Rotate(transform.up, turnSpeed * factor * 90f / turn90Duration);
            yield return new WaitForSeconds(factor);
        }
    }
}
