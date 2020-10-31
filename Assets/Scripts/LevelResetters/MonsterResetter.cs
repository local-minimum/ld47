using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterResetter : LevelResetter
{
    [SerializeField] Transform monsterSpawn;
    Monster monster;

    private void Start()
    {
        monster = GetComponentInChildren<Monster>();
    }

    protected override void ResetMe()
    {
        monster.TeleportTo(monsterSpawn);
        monster.Hunt(null);
        monster.ForgetPlayer();
    }
}
