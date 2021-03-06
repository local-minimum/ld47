﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    static Interactable focused;

    [SerializeField, Range(0, 5f)]
    float actionDistance = 1f;
    
    UILocation[] uiAnchor;

    [SerializeField, Range(0, 3f)]
    float uiLingerTime = 0.2f;

    float lastLookedUpon = 0f;

    public bool interactable { get; set; }
    Transform bestAnchor;
    //Vector3 looker;


    private void Start()
    {
        uiAnchor = GetComponentsInChildren<UILocation>();
    }

    void Update()
    {
        if (focused != this) return;
        if (Time.timeSinceLevelLoad - lastLookedUpon > uiLingerTime || !interactable)
        {
            focused = null;            
            UIInteractable.Hide();
        }
    }

    public void LookedAt(Vector3 lookerPos, Vector3 rayDirection)
    {
        if (uiAnchor.Length == 0)
        {
            Debug.LogWarning(name);
            return;
        }
        if (!interactable) return;

        if (focused != this)
        {
            bestAnchor = uiAnchor[0].transform;
            float bestAngle = 180;
            for (int i=0; i<uiAnchor.Length; i++)
            {
                float angle = Mathf.Abs(Vector3.Angle(uiAnchor[i].transform.forward, rayDirection));
                if (angle < bestAngle)
                {
                    bestAngle = angle;
                    bestAnchor = uiAnchor[i].transform;
                }
            }
            UIInteractable.Place(bestAnchor);
            focused = this;
        }
        float dist = Vector3.Distance(lookerPos, bestAnchor.position);
        //looker = lookerPos;        
        UIInteractable.canInteract = dist <= actionDistance;

        lastLookedUpon = Time.timeSinceLevelLoad;
    }
    /*
    private void OnDrawGizmos()
    {
        if (bestAnchor != null && focused == this)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(looker, bestAnchor.position);            
        }
    }
    */
    private void OnDestroy()
    {
        if (focused == this) { focused = null; }
    }
}
