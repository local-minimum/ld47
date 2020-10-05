using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseWall : MonoBehaviour
{
    [SerializeField]
    AudioClip fallSound;

    bool charged = false;

    public void FallDown()
    {
        if (charged)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<MeshCollider>().enabled = true;
            GetComponent<AudioSource>().PlayOneShot(fallSound);
        }
    }

    public void SetTrap()
    {
        charged = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
    }
}
