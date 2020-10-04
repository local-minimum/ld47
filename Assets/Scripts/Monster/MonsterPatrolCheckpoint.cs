using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolCheckpoint : MonoBehaviour
{
    [SerializeField]
    MonsterPatrolCheckpoint nextCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Monster monster = other.GetComponentInChildren<Monster>();            
            if (monster != null)
            {                
                if (nextCheckpoint != null)
                {
                    monster.Hunt(nextCheckpoint.transform);
                }
            }
        }
    }
}
