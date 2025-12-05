using UnityEngine;

public enum PlayerStateID
{
    Root,
    
    Grounded,
    Idle,
    Move,
    
    Airborne,
    Jump,
    Fall,

}

public class PlayerState : State
{
    public PlayerContext ctx;
    public PlayerRoot root => (PlayerRoot)machine.root;

    public PlayerState(State parent, PlayerContext ctx) : base(parent)
    {
        this.ctx = ctx;
    }
}
