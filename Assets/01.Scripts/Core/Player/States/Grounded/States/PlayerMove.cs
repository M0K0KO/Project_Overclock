public class PlayerMove : PlayerState
{
    public PlayerMove(StateMachine m, State parent, PlayerContext ctx) : base(parent, ctx)
    {
        PlayerStatesRegistry.Register(PlayerStateID.Move, this);
    }

    protected override State GetTransition()
    {
        return ctx.movementContext.horizontalVelocity < 1f ? PlayerStatesRegistry.Get(PlayerStateID.Idle) : null;
    }

    protected override void OnEnter()
    {
        
    }
}