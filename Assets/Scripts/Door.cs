using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    float yRotationClosed;
    [SerializeField]
    float yRotationOpened;
    float currentRotation;
    float currentDuration;

    [SerializeField, Range(0, 2)]
    float animationDuration;

    bool isOpen = false;
    int toggleIteraion = 0;

    public void Toggle()
    {
        toggleIteraion++;
        StartCoroutine(DoToggle(toggleIteraion));
    }

    IEnumerator<WaitForSeconds> DoToggle(int iteration) {
        
        float startAngle = isOpen ? yRotationClosed : currentRotation;
        float timeDirection = isOpen ? -1 : 1;
        float endAngle = isOpen ? currentRotation : yRotationOpened;
        Debug.Log(string.Format("{0} -> {1}, t: {2} / {3}", startAngle, endAngle, currentDuration, timeDirection));
        isOpen = !isOpen;
        while (toggleIteraion == iteration && currentDuration >= 0 && currentDuration <= animationDuration)
        {
            currentRotation = Mathf.LerpAngle(startAngle, endAngle, currentDuration / animationDuration);
            transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
            yield return new WaitForSeconds(0.02f);
            currentDuration += 0.02f * timeDirection;
        }
        if (toggleIteraion == iteration)
        {
            currentDuration = Mathf.Clamp01(currentDuration);
            currentRotation = Mathf.LerpAngle(startAngle, endAngle, currentDuration / animationDuration);
            transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);            
        }        
    }

    private void Start()
    {
        currentRotation = yRotationClosed;
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
