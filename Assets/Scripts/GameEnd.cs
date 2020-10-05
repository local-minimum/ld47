using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    [SerializeField] GameObject[] activateOnEnter;

    private void Start()
    {
        for (int i = 0; i < activateOnEnter.Length; i++)
        {
            activateOnEnter[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i=0; i<activateOnEnter.Length; i++)
            {
                activateOnEnter[i].SetActive(true);
            }
        }
    }
}
