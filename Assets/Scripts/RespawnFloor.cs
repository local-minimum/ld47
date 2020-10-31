using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFloor : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
            other.transform.position = respawnPoint.position;
            other.transform.rotation = respawnPoint.rotation;
        }
    }
}
