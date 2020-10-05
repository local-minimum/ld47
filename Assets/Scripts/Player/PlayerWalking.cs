using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Cursor.visible = false;
    }

    [SerializeField, Range(0, 10f)]
    float lateralLook = 2f;

    [SerializeField, Range(0, 10f)]
    float lookDistance = 5f;

    [SerializeField, Range(0, 2f)]
    float verticalPosLook = 0.5f;

    [SerializeField, Range(0, 2f)]
    float verticalNegLook = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (killed)
        {
            turnSpeed = 0f;
            walkSpeed = 0f;
            return;
        }
        walkSpeed = Input.GetAxis("Vertical");
        turnSpeed = Input.GetAxis("Horizontal");
        float xLookFactor = Input.mousePosition.x / Screen.width - 0.5f;
        float yLookFactor = Input.mousePosition.y / Screen.height - 0.5f;
        Vector3 lookAt = transform.position
            + transform.forward * lookDistance
            + xLookFactor * lateralLook * transform.right
            + (yLookFactor > 0 ? yLookFactor * verticalPosLook * transform.up : Vector3.zero)
            + (yLookFactor < 0 ? yLookFactor * verticalNegLook * transform.up : Vector3.zero)
            ;
        eyes.LookAt(lookAt);
        phone.transform.LookAt(lookAt);
    }

    bool killed = false;

    IEnumerator<WaitForSeconds> Step()
    {
        float factor = 0.02f;
        while (!killed) {
            if (Mathf.Abs(walkSpeed) > 0.1f) {
                float duration = 0f;
                float stepSpeed = Mathf.Abs(walkSpeed);
                while (duration < stepDuration && !killed)
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

    public void SetKilled()
    {
        if (killed) return;
        killed = true;

        StartCoroutine(Kill());
    }

    IEnumerator<WaitForSeconds> Kill()
    {
        Camera cam = eyes.GetComponent<Camera>();
        float step = 0.5f;
        while (cam.fieldOfView > 21)
        {
            cam.fieldOfView -= step;
            step += .1f;
            yield return new WaitForSeconds(0.02f);
        }
        SceneManager.LoadScene("Scenes/Level");
    }
}
