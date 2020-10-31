using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : LevelResetter
{
    [SerializeField] Transform respawnPosition;

    PlayerWalking player;

    private void Start()
    {
        player = GetComponentInChildren<PlayerWalking>();    
    }
    protected override void ResetMe()
    {
        transform.SetParent(null);
        transform.position = respawnPosition.position;
        transform.rotation = respawnPosition.rotation;
        player.UnKill();
    }
}
