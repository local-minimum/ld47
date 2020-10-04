using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLocationRoom : MonoBehaviour
{
    [SerializeField]
    Transform[] locations;

    public void TeleportTo(int location)
    {
        Transform target = locations[location];
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
