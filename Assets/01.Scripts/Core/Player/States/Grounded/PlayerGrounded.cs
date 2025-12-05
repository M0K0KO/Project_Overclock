public class PlayerGrounded : PlayerState
{
    public readonly PlayerIdle idle;
    public readonly PlayerMove move;

    public PlayerGrounded(StateMachine m, State parent, PlayerContext ctx) : base(parent, ctx)
    {
        PlayerStatesRegistry.Register(PlayerStateID.Grounded, this);
        
        idle = new PlayerIdle(m, this, ctx);
        move = new PlayerMove(m, this, ctx);
    }

    protected override State GetInitialState() => PlayerStatesRegistry.Get(PlayerStateID.Idle);

    protected override State GetTransition() => ctx.movementContext.isGrounded ? null : PlayerStatesRegistry.Get(PlayerStateID.Airborne);

    protected override void OnEnter()
    {
        if (ctx.movementContext.jumpCount == 1) ctx.animDriver.PlayLocomotion("Landing1", 0.2f);
        if (ctx.movementContext.jumpCount == 2) ctx.animDriver.PlayLocomotion("Landing2", 0.3f);

        ctx.movementContext.jumpCount = 0;
    }
}