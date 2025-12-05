public class PlayerRoot : PlayerState
{
    public readonly PlayerGrounded grounded;
    public readonly PlayerAirborne airborne;
    private readonly PlayerContext ctx;

    public PlayerRoot(StateMachine machine, PlayerContext ctx) : base(null, null)
    {
        this.ctx = ctx;

        PlayerStatesRegistry.Register(PlayerStateID.Root, this);
        
        grounded = new PlayerGrounded(machine, this, ctx);
        airborne = new PlayerAirborne(machine, this, ctx);
    }

    protected override State GetInitialState() => PlayerStatesRegistry.Get(PlayerStateID.Grounded);

    protected override State GetTransition()
    {
        bool onGround = ctx.movementContext.isGrounded;

        if (onGround)
        {
            if (activeChild == grounded) return null;
            return PlayerStatesRegistry.Get(PlayerStateID.Grounded);
        }
        else
        {
            if (activeChild == airborne) return null;
            return PlayerStatesRegistry.Get(PlayerStateID.Airborne);
        }
    }
}