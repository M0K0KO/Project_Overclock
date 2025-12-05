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
        ctx.animDriver.PlayLocomotion("Jump", 0.2f);
    }

    protected override void OnUpdate(float deltaTime)
    {
        ctx.animDriver.UpdateLocomotionParams(ctx.movementContext.velocity, ctx.movementContext.isGrounded, ctx.movementContext.jumpCount);

        if (ctx.movementContext.shouldPlayDoubleJumpAnimation)
        {
            ctx.movementContext.shouldPlayDoubleJumpAnimation = false;
            ctx.animDriver.PlayLocomotion("Jump", 0.2f);
        }
    }
}