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
            if (monster != null && monster.IsTargeting(transform))
            {                
                Debug.Log(string.Format("Monster reached {0}", name));
                if (nextCheckpoint != null)
                {
                    monster.Hunt(nextCheckpoint.transform);                    
                }
            }
        }
    }
}
