using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFalseWall : LevelResetter
{
    FalseWall wall;

    private void Start()
    {
        wall = GetComponentInChildren<FalseWall>();
    }

    protected override void ResetMe()
    {
        wall.FallDown(false);
    }
}
