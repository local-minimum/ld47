using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    [SerializeField]
    Door[] doors;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
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
    }
}
