using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseWallCharger : MonoBehaviour
{
    [SerializeField]
    FalseWall wall;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log(other.tag);
            wall.SetTrap();
        }
    }
}
