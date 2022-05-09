using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class GameManager : Singleton<GameManager>
{
    public PlayerState playerState;
    public void RigisterPlayer(PlayerState player)
    {
        playerState = player;
    }
}
