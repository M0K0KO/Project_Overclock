using System;
using UnityEngine;

public class PlayerActor : Actor
{
    public override void Initialize()
    {
        actorContext = new PlayerContext();
        actorType = ActorType.Player;
        
        base.Initialize();
        
        var playerContext = (PlayerContext)actorContext;
        playerContext.playerInput = GetComponent<PlayerInput>();
        playerContext.playerCam = Camera.main;
    }
}
