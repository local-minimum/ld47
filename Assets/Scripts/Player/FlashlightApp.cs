using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightApp : MonoBehaviour
{
    [SerializeField]
    KeyCode actionKey = KeyCode.X;

    [SerializeField]
    Light spotLight;

    bool isOn = false;

    [SerializeField]
    AnimationCurve transitionIntensity;

    [SerializeField, Range(0, 2)]
    float transitionDuration = 1f;
    
    Image flashlightImage;

    [SerializeField]
    Color lightOnTextColor;

    [SerializeField]
    Color lightOffTextColor;
    
    TMPro.TextMeshProUGUI textElement;

    bool transitioning = false;

    private void Start()
    {
        flashlightImage = GetComponentInChildren<Image>();
        textElement = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if (!transitioning) StartCoroutine(Transition());
        }
    }

    IEnumerator<WaitForSeconds> Transition()
    {
        transitioning = true;        
        float startTrans = isOn ? 1f : 0f;
        float factor = isOn ? -1 / transitionDuration : 1 / transitionDuration;
        flashlightImage.color = isOn ? lightOffTextColor : lightOnTextColor;
        textElement.color = isOn ? lightOffTextColor : lightOnTextColor;
        float startTime = Time.timeSinceLevelLoad;
        float duration = 0;
        while (duration < transitionDuration) {
            float intensity = transitionIntensity.Evaluate(startTrans + duration * factor);
            spotLight.intensity = intensity;
            yield return new WaitForSeconds(0.02f);
            duration = Time.timeSinceLevelLoad - startTime;
        }        
        
        isOn = !isOn;
        spotLight.intensity = isOn ? 1f : 0f;
        transitioning = false;
    }
}
