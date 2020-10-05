using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    [SerializeField]
    Door[] doors;

    [SerializeField]
    bool closeOnEnter;

    void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            Door door = doors[i];
            if (door.isOpen)
            {
                door.Toggle();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!closeOnEnter && other.tag == "Player")
        {
            CloseDoors();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if  (closeOnEnter && other.tag == "Player")
        {
            CloseDoors();
        }
    }
}
