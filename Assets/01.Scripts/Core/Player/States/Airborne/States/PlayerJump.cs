using UnityEngine;

public class PlayerJump : PlayerState
{
    public PlayerJump(StateMachine m, State parent, PlayerContext ctx) : base(parent, ctx)
    {
        PlayerStatesRegistry.Register(PlayerStateID.Jump, this);
    }

    protected override State GetTransition() => 
        ctx.movementContext.verticalVelocity < 0f ? PlayerStatesRegistry.Get(PlayerStateID.Fall) : null; 

    protected override void OnEnter()
    {
    }
}