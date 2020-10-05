using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextApp : MonoBehaviour
{
    [SerializeField]
    GameObject appContents;

    [SerializeField]
    TMPro.TextMeshProUGUI fromText;

    [SerializeField]
    TMPro.TextMeshProUGUI bodyText;

    FlashlightApp flashlightApp;
    Eyes eyes;

    bool isVisible;

    static public void RecieveMessage(string from, string body)
    {
        if (instance == null)
        {
            Debug.LogWarning(string.Format("Lost message '{0}' from '{1}'", body, from));
            return;
        }
        instance.DisplayMessage(from, body);
    }

    static TextApp instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            flashlightApp = FindObjectOfType<FlashlightApp>();
            eyes = FindObjectOfType<Eyes>();
        } else
        {
            Destroy(gameObject);
        }
    }

    void DisplayMessage(string from, string body)
    {
        flashlightApp.enabled = false;
        fromText.text = from;
        bodyText.text = body;
        appContents.SetActive(true);
        isVisible = true;
        StartCoroutine(FocusOnPhone());
    }

    [SerializeField, Range(20, 60)]
    float focusFOV = 25f;

    [SerializeField]
    AnimationCurve fovTransition;

    IEnumerator<WaitForSeconds> FocusOnPhone() {
        Camera cam = eyes.GetComponent<Camera>();
        float start = Time.timeSinceLevelLoad;
        float duration = 0;
        while (isVisible && duration < 1f)
        {
            cam.fieldOfView = Mathf.Lerp(60, focusFOV, fovTransition.Evaluate(duration));
            yield return new WaitForSeconds(0.02f);
            duration = Time.timeSinceLevelLoad - start;
        }
        if (isVisible) cam.fieldOfView = focusFOV;
    }

    IEnumerator<WaitForSeconds> DeFocusOnPhone()
    {
        Camera cam = eyes.GetComponent<Camera>();
        float start = Time.timeSinceLevelLoad;
        float duration = 0;
        while (!isVisible && duration < 1f)
        {
            cam.fieldOfView = Mathf.Lerp(focusFOV, 60f, fovTransition.Evaluate(duration));
            yield return new WaitForSeconds(0.02f);
            duration = Time.timeSinceLevelLoad - start;
        }
        if (!isVisible) cam.fieldOfView = 60;
    }

    void Update()
    {
        if (!isVisible) return;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            isVisible = false;
            appContents.SetActive(false);
            flashlightApp.enabled = true;
            StartCoroutine(DeFocusOnPhone());
        }
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

}
