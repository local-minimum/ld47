using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    AudioClip[] stepSounds;
    int stepSoundIdx = 0;

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
        AudioSource speakers = GetComponent<AudioSource>();
        float factor = 0.02f;
        while (!killed) {
            if (Mathf.Abs(walkSpeed) > 0.1f) {
                float duration = 0f;
                float stepSpeed = Mathf.Abs(walkSpeed);
                bool playedSound = false;
                while (duration < stepDuration && !killed)
                {
                    float progress = duration / stepDuration;
                    eyeCamera.transform.localPosition = cameraOrigin + transform.up * stepHeight.Evaluate(progress);
                    yield return new WaitForSeconds(factor);
                    stepSpeed = Mathf.Max(stepSpeed, Mathf.Abs(walkSpeed));
                    duration += stepSpeed * factor;
                    transform.position += transform.forward * walkSpeed * stepLength * factor;
                    transform.Rotate(transform.up, turnSpeed * factor * 90f / turn90Duration);
                    if (progress > 0.9f && !playedSound && walkSpeed > 0f)
                    {
                        speakers.PlayOneShot(stepSounds[stepSoundIdx]);
                        playedSound = true;
                    }
                }
                stepSoundIdx += 1;
                if (stepSoundIdx >= stepSounds.Length)
                {
                    stepSoundIdx = 0;
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

    [SerializeField]
    AudioClip scream;

    IEnumerator<WaitForSeconds> Kill()
    {
        AudioSource speakers = GetComponent<AudioSource>();
        GameObject killScreen = GameObject.FindGameObjectWithTag("KillScreen");
        Image killImage = killScreen.GetComponentInChildren<Image>();
        Color color = Color.black;
        color.a = 0f;
        Camera cam = eyes.GetComponent<Camera>();
        float step = 0.5f;
        speakers.PlayOneShot(scream);

        while (cam.fieldOfView > 21)
        {
            cam.fieldOfView -= step;
            step += .1f;
            color.a = step;
            killImage.color = color;
            yield return new WaitForSeconds(0.02f);
        }
        SceneManager.LoadScene("Scenes/Level");
    }
}
