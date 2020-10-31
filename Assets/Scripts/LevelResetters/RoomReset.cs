using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReset : LevelResetter
{
    Vector3 resetPos;
    Quaternion resetRotation;
    

    void Start()
    {
        resetPos = transform.position;
        resetRotation = transform.rotation;       
    }

    protected override void ResetMe()
    {
        transform.position = resetPos;
        transform.rotation = resetRotation;
    }
}
