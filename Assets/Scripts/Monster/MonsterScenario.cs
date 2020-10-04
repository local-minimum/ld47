using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScenario : MonoBehaviour
{
    [SerializeField]
    Monster monster;

    [SerializeField]
    Transform monsterSpawn;

    [SerializeField]
    MonsterPatrolCheckpoint firstCheckpoint;

    [SerializeField, Range(0, 4)]
    float patrolSpeed = 1.5f;

    [SerializeField, Range(0, 4)]
    float huntSpeed = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            monster.TeleportTo(monsterSpawn);

            if (firstCheckpoint != null)
            {                
                monster.Hunt(firstCheckpoint.transform);
            }
            monster.huntSpeed = huntSpeed;
            monster.patrolSpeed = patrolSpeed;
        }
    }
}
