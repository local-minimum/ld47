using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    float detectionRange = 10f;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f)) {
            Interactable inter = hit.collider.gameObject.GetComponent<Interactable>();
            if (inter != null)
            {
                inter.LookedAt(hit.distance);
            }
        }
    }
}
