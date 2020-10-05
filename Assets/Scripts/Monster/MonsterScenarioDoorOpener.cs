using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorOpener
{
    PLAYER,
    MONSTER,
    MONSTER_OR_PLAYER,
}

public class MonsterScenarioDoorOpener : MonoBehaviour
{
    [SerializeField]
    Door[] doors;

    [SerializeField]
    DoorOpener opener;

    [SerializeField]
    string scenario;

    private void OnTriggerEnter(Collider other)
    {
        if (MonsterScenario.CurrentScenario == scenario)
        {
            if (opener == DoorOpener.PLAYER && other.tag != "Player") return;
            if (opener == DoorOpener.MONSTER && other.tag != "Monster") return;
            // DoorOpener.MONSTER_OR_PLAYER
            if (other.tag != "Player" && other.tag != "Monster") return;

            for (int i=0; i<doors.Length; i++)
            {
                if (!doors[i].isOpen) doors[i].Toggle();
            }
        }
    }
}
