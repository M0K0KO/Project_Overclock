using UnityEngine;

public class PlayerRoot : State
{
    public readonly Grounded grounded;
    public readonly Airborne airborne;
    private readonly PlayerContext ctx;

    public PlayerRoot(StateMachine machine, PlayerContext ctx) : base(null)
    {
        this.ctx = ctx;
        grounded = new Grounded(machine, this, ctx);
        airborne = new Airborne(machine, this, ctx);
    }

    protected override State GetInitialState() => grounded;
    protected override State GetTransition() => ctx.movementContext.isGrounded ? grounded : airborne;
}

public class Airborne : State
{
    readonly PlayerContext ctx;

    public Airborne(StateMachine m, State parent, PlayerContext ctx) : base(parent)
    {
        this.ctx = ctx;
    }

    protected override State GetTransition() => ctx.movementContext.isGrounded ? ((PlayerRoot)parent).grounded : null;

    protected override void OnEnter()
    {
    }
}

public class Grounded : State
{
    readonly PlayerContext ctx;
    public readonly Idle idle;
    public readonly Move move;

    public Grounded(StateMachine m, State parent, PlayerContext ctx) : base(parent)
    {
        this.ctx = ctx;
        idle = new Idle(m, this, ctx);
        move = new Move(m, this, ctx);
    }

    protected override State GetInitialState() => idle;

    protected override State GetTransition() => ctx.movementContext.isGrounded ? null : ((PlayerRoot)parent).grounded;

    protected override void OnEnter()
    {
    }
}

public class Idle : State
{
    readonly PlayerContext ctx;

    public Idle(StateMachine m, State parent, PlayerContext ctx) : base(parent)
    {
        this.ctx = ctx;
    }

    protected override State GetTransition() => 
        ctx.movementContext.horizontalVelocity > 0.01f ? ((Grounded)parent).move : null; 

    protected override void OnEnter()
    {
        ctx.movementContext.velocity = Vector3.zero;
    }
}

public class Move : State
{
    readonly PlayerContext ctx;

    public Move(StateMachine m, State parent, PlayerContext ctx) : base(parent)
    {
        this.ctx = ctx;
    }

    protected override State GetTransition()
    {
        if (!ctx.movementContext.isGrounded) return ((PlayerRoot)parent).airborne;
        return ctx.movementContext.horizontalVelocity <= 0.01f ? ((Grounded)parent).idle : null;
    }

    protected override void OnEnter()
    {
        
    }
}