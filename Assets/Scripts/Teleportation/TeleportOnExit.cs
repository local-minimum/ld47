using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TeleportConfig
{
    public Transform referencePoint;
    public int roomPosition;
}


public class TeleportOnExit : MonoBehaviour
{
    [SerializeField]
    TeleportConfig[] configs;

    [SerializeField]
    MultiLocationRoom room;

    private void OnTriggerExit(Collider other)
    {
        if  (other.tag == "Player")
        {
            int roomPosition = -1;
            float sqDist = 0;
            for (int i = 0; i < configs.Length; i++)
            {
                TeleportConfig config = configs[i];
                float curSqDist = Vector3.SqrMagnitude(config.referencePoint.position - other.transform.position);
                if (roomPosition < 0 || curSqDist < sqDist)
                {
                    roomPosition = config.roomPosition;
                    sqDist = curSqDist;
                }
            }
            if (roomPosition == -1)
            {
                Debug.LogError("Could not find an exit");
            } else
            {
                room.TeleportTo(roomPosition);
            }
        }
    }
}
