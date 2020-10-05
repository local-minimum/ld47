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

    [SerializeField]
    AudioClip openAcceptSound;

    [SerializeField]
    AudioClip openRefusedSound;

    [SerializeField]
    AudioClip doorSlideOpen;

    [SerializeField]
    AudioClip doorSlideClose;

    AudioSource speakers;

    public void Toggle()
    {
        toggleIteraion++;
        StartCoroutine(DoToggle(toggleIteraion));
    }

    IEnumerator<WaitForSeconds> DoToggle(int iteration) {
        if (!locked || isOpen)
        {
            if (!isOpen) speakers.PlayOneShot(openAcceptSound);
            float duration = isOpen ? 1f : 0f;
            float timeDirection = isOpen ? -1f : 1f;
            GetComponentInChildren<Interactable>().interactable = isOpen;
            speakers.clip = isOpen ? doorSlideClose : doorSlideOpen;
            speakers.Play();
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
                speakers.Stop();
            }
        } else if (locked)
        {
            speakers.PlayOneShot(openRefusedSound);
        }
    }


    private void Start()
    {
        speakers = GetComponent<AudioSource>();
        if (speakers == null)
        {
            speakers = gameObject.AddComponent<AudioSource>();
        }
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
        if (uIInteractable && uIInteractable.showingKey && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            Toggle();
        }
    }
}
