using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DoorLockConfig
{
    public Door door;
    public bool locked;
    public bool doClose;

}

public class DoorLocker : MonoBehaviour
{
    [SerializeField]
    DoorLockConfig[] configs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i =0; i< configs.Length; i++)
            {
                DoorLockConfig config = configs[i];
                if (config.doClose && config.door.isOpen)
                {
                    config.door.Toggle();
                }
                config.door.locked = config.locked;
            }
        }
    }
}
