using UnityEngine;

public class PlayerFall : PlayerState
{
    public PlayerFall(StateMachine m, State parent, PlayerContext ctx) : base(parent, ctx)
    {
        PlayerStatesRegistry.Register(PlayerStateID.Fall, this);
    }

    protected override State GetTransition() => 
        ctx.movementContext.verticalVelocity > 0f ? PlayerStatesRegistry.Get(PlayerStateID.Jump) : null; 

    protected override void OnEnter()
    {
        ctx.animDriver.PlayLocomotion("Fall", 0.2f);
    }
    
    protected override void OnUpdate(float deltaTime)
    {
        ctx.animDriver.UpdateLocomotionParams(ctx.movementContext.velocity, ctx.movementContext.isGrounded, ctx.movementContext.jumpCount);
    }
}