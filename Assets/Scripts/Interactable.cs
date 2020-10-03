using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    static Interactable focused;

    [SerializeField, Range(0, 5f)]
    float actionDistance = 1f;

    [SerializeField]
    Transform[] uiAnchor;

    [SerializeField, Range(0, 3f)]
    float uiLingerTime = 0.2f;

    float lastLookedUpon = 0f;
    

    // Update is called once per frame
    void Update()
    {
        if (focused == this && Time.timeSinceLevelLoad - lastLookedUpon > uiLingerTime)
        {
            focused = null;
            UIInteractable.Hide();
        }
    }

    public void LookedAt(float distance, Vector3 rayDirection)
    {
        if (focused != this)
        {
            Transform bestAnchor = uiAnchor[0];
            float bestAngle = 180;
            for (int i=0; i<uiAnchor.Length; i++)
            {
                float angle = Mathf.Abs(Vector3.Angle(uiAnchor[i].transform.forward, rayDirection));
                if (angle < bestAngle)
                {
                    bestAngle = angle;
                    bestAnchor = uiAnchor[i];
                }
            }
            UIInteractable.Place(bestAnchor);
            focused = this;
        }
        UIInteractable.canInteract = distance <= actionDistance;
        lastLookedUpon = Time.timeSinceLevelLoad;
    }

    private void OnDestroy()
    {
        if (focused == this) { focused = null; }
    }
}
