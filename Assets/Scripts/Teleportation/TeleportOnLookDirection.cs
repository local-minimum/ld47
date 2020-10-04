using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnLookDirection : MonoBehaviour
{
    [SerializeField]
    TeleportConfig[] configs;

    [SerializeField]
    MultiLocationRoom room;


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Eyes eyes = other.GetComponentInChildren<Eyes>();            
            if (eyes != null)
            {
                Vector3 lookDirection = eyes.transform.forward;
                Vector3 playerPos = other.transform.position;

                int roomPosition = -1;
                float bestAngle = 0;
                for (int i = 0; i < configs.Length; i++)
                {
                    TeleportConfig config = configs[i];
                    Vector3 configDirection = config.referencePoint.position - playerPos;
                    float angle = Mathf.Abs(Vector3.Angle(lookDirection, configDirection));
                    if (roomPosition < 0 || angle < bestAngle)
                    {
                        bestAngle = angle;
                        roomPosition = config.roomPosition;
                    }
                }
                if (roomPosition < 0)
                {
                    Debug.LogError("Teleport on Look Direction didn't find any configs");
                }
                else
                {
                    room.TeleportTo(roomPosition);
                }
            }
        }
    }
}
