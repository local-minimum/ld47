using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseWallTrapper : MonoBehaviour
{
    [SerializeField]
    FalseWall wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wall.FallDown();
        }
    }
}
