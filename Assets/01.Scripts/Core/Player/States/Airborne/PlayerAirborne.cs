using UnityEngine;

public class PlayerAirborne : PlayerState
{
    public readonly PlayerJump jump;
    public readonly PlayerFall fall;
    
    public PlayerAirborne(StateMachine m, State parent, PlayerContext ctx) : base(parent, ctx)
    {
        PlayerStatesRegistry.Register(PlayerStateID.Airborne, this);
        
        jump = new PlayerJump(m, this, ctx);
        fall = new PlayerFall(m, this, ctx);
    }

    protected override State GetInitialState()
    { 
        return ctx.movementContext.verticalVelocity < 0f
            ? PlayerStatesRegistry.Get(PlayerStateID.Fall)
            : PlayerStatesRegistry.Get(PlayerStateID.Jump);
    }

    protected override State GetTransition()
    {
        return ctx.movementContext.isGrounded ? PlayerStatesRegistry.Get(PlayerStateID.Grounded) : null;
    }

    protected override void OnEnter()
    {
    }
}
