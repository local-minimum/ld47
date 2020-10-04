using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    float detectionRange = 10f;

    [SerializeField]
    LayerMask interactableLayers;

    Vector3 lastLook;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, interactableLayers)) {
            lastLook = hit.point;
            // Debug.Log(hit.collider.name);
            Interactable inter = hit.collider.gameObject.GetComponent<Interactable>();
            if (inter != null)
            {
                inter.LookedAt(transform.position, transform.forward);
            }
        } /*else
        {
            Debug.Log("Hit nothing");
            lastLook = new Ray(transform.position, transform.forward).GetPoint(detectionRange);
        } */
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, lastLook);
        Debug.Log(Vector3.Distance(transform.position, lastLook));
    }*/

    public void LookAt(Vector3 pos)
    {
        transform.LookAt(pos);
    }
}
