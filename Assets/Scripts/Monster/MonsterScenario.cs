using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerCondition
{
    ALWAYS,
    ALWAYS_UNLESS_PREVIOUS,
    REQUIRES_PREVIOUS,
}

public class MonsterScenario : MonoBehaviour
{
    public static string CurrentScenario { get; private set; }

    Monster monster;

    [SerializeField]
    Transform monsterSpawn;

    [SerializeField]
    MonsterPatrolCheckpoint firstCheckpoint;

    [SerializeField, Range(0, 4)]
    float patrolSpeed = 1.5f;

    [SerializeField, Range(0, 4)]
    float huntSpeed = 3f;

    [SerializeField]
    TriggerCondition triggerCondition;

    [SerializeField]
    string[] previousScenarios;

    [SerializeField]
    bool doNotTeleport;

    private void Start()
    {
        monster = FindObjectOfType<Monster>();
    }

    bool requirementsSatisfied
    {
        get
        {
            if (triggerCondition == TriggerCondition.ALWAYS) return true;
            if (triggerCondition == TriggerCondition.REQUIRES_PREVIOUS)
            {
                for (int i = 0; i < previousScenarios.Length; i++)
                {
                    string previousScenario = previousScenarios[i];
                    if (CurrentScenario == previousScenario || string.IsNullOrEmpty(CurrentScenario) == string.IsNullOrEmpty(previousScenario)) return true;                    
                }
                return false;
            } else
            {
                // TriggerCondition.ALWAYS_UNLESS_PREVIOUS
                for (int i = 0; i < previousScenarios.Length; i++)
                {
                    string previousScenario = previousScenarios[i];
                    if (CurrentScenario == previousScenario || string.IsNullOrEmpty(CurrentScenario) == string.IsNullOrEmpty(previousScenario)) return false;
                }
                return true;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CurrentScenario != name && requirementsSatisfied)
        {
            Debug.Log(string.Format("Playing Scenario {0}", name)); 

            if (!doNotTeleport) monster.TeleportTo(monsterSpawn);

            if (firstCheckpoint != null)
            {                
                monster.Hunt(firstCheckpoint.transform);
            }
            monster.huntSpeed = huntSpeed;
            monster.patrolSpeed = patrolSpeed;
            CurrentScenario = name;
        }
    }
}
