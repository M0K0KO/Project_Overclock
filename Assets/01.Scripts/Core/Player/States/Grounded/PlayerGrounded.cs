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
    }
}