using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Transform slider;

    [SerializeField, Range(0, 2)]
    float animationDuration = 1;

    public bool isOpen { get; private set; }
    int toggleIteraion = 0;
    public bool locked { get; set; }

    Vector3 sliderLocalLocked;
    [SerializeField, Range(0, 3)]
    float slideDistance = 1f;

    public void Toggle()
    {
        toggleIteraion++;
        StartCoroutine(DoToggle(toggleIteraion));
    }

    IEnumerator<WaitForSeconds> DoToggle(int iteration) {
        if (!locked || isOpen)
        {
            float duration = isOpen ? 1f : 0f;
            float timeDirection = isOpen ? -1f : 1f;
            GetComponentInChildren<Interactable>().interactable = isOpen;
            isOpen = !isOpen;
            while (toggleIteraion == iteration && duration >= 0 && duration <= animationDuration)
            {
                slider.localPosition = Vector3.Lerp(sliderLocalLocked, sliderLocalLocked + Vector3.right * slideDistance, duration / animationDuration);                
                yield return new WaitForSeconds(0.02f);
                duration += 0.02f * timeDirection;
            }
            if (toggleIteraion == iteration)
            {
                duration = Mathf.Clamp01(duration);
                slider.localPosition = Vector3.Lerp(sliderLocalLocked, sliderLocalLocked + Vector3.right * slideDistance, duration / animationDuration);                
            }
        }
    }


    private void Start()
    {
        slider = transform.GetChild(0);
        sliderLocalLocked = slider.transform.localPosition;
        Interactable interactable = GetComponentInChildren<Interactable>();
        if (interactable != null)
        {
            interactable.interactable = true;
        }
        
    }

    private void Update()
    {
        UIInteractable uIInteractable = GetComponentInChildren<UIInteractable>();
        if (uIInteractable && uIInteractable.showingKey && Input.GetKeyDown(KeyCode.C)) 
        {
            Toggle();
        }
    }
}
