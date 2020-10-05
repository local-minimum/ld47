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

    [SerializeField]
    bool actuallyCloseDoorInstead;

    private void OnTriggerStay(Collider other)
    {
        if (opener == DoorOpener.PLAYER && other.tag != "Player") return;
        if (opener == DoorOpener.MONSTER && other.tag != "Monster") return;
        // DoorOpener.MONSTER_OR_PLAYER
        if (other.tag != "Player" && other.tag != "Monster") return;

        if (MonsterScenario.CurrentScenario == scenario)
        {

            for (int i = 0; i < doors.Length; i++)
            {
                //Debug.Log(string.Format("{0} is opening door {1}", other.tag, doors[i].name));
                if (!actuallyCloseDoorInstead && !doors[i].isOpen)
                {
                    doors[i].Toggle();
                } else if (actuallyCloseDoorInstead && doors[i].isOpen)
                {
                    doors[i].Toggle();
                }

            }
        }
        else {
            //Debug.Log(string.Format("{0} is not the needed scenario {1}", MonsterScenario.CurrentScenario, scenario));
        }
    }
}
