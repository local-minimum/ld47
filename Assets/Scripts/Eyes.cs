using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField, Range(1, 20)]
    float detectionRange = 10f;

    [SerializeField]
    LayerMask interactableLayers;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, interactableLayers)) {
            Interactable inter = hit.collider.gameObject.GetComponent<Interactable>();
            if (inter != null)
            {
                inter.LookedAt(hit.distance, transform.forward);
            }
        }        
    }

    public void LookAt(Vector3 pos)
    {
        transform.LookAt(pos);
    }
}
