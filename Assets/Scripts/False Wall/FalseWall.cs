using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseWall : MonoBehaviour
{
    [SerializeField]
    AudioClip fallSound;

    bool charged = false;

    public void FallDown(bool playSound=true)
    {
        if (charged)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<MeshCollider>().enabled = true;
            if (playSound)
            {
                GetComponent<AudioSource>().PlayOneShot(fallSound);
            }
            charged = false;
        }
    }

    public void SetTrap()
    {
        charged = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
    }
}
