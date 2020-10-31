using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LevelResetter.ResetLevel();
    }
}
