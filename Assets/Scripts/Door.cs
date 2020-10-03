using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    float yRotationClosed;
    [SerializeField]
    float yRotationOpened;

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
        float start = Time.timeSinceLevelLoad;
        float duration = 0;
        float startAngle = isOpen ? yRotationOpened : yRotationClosed;
        float endAngle = isOpen ? yRotationClosed : yRotationOpened;
        while (toggleIteraion == iteration && duration < animationDuration)
        {            
            transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(startAngle, endAngle, duration / animationDuration), Vector3.up);
            yield return new WaitForSeconds(0.02f);
            duration = Time.timeSinceLevelLoad - start;
        }
        if (toggleIteraion == iteration)
        {
            transform.rotation = Quaternion.AngleAxis(endAngle, Vector3.up);
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
