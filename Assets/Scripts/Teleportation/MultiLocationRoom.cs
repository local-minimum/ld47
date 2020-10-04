using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLocationRoom : MonoBehaviour
{
    [SerializeField]
    Transform[] locations;
    [SerializeField]
    bool rotate;

    public void TeleportTo(int location)
    {
        Transform target = locations[location];
        transform.position = target.position;
        if (rotate)
        {
            transform.rotation = target.rotation;
        }
    }
}
