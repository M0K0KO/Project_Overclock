using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(StateMachine m, State parent, PlayerContext ctx) : base(parent, ctx)
    {
        PlayerStatesRegistry.Register(PlayerStateID.Idle, this);
    }

    protected override State GetTransition()
    {
        return ctx.movementContext.horizontalVelocity > 1f ? PlayerStatesRegistry.Get(PlayerStateID.Move) : null; 
    }

    protected override void OnEnter()
    {
    }
}