using System.Collections.Generic;

public abstract class State
{
    public StateMachine machine { get; private set; }
    public readonly State parent;
    public State activeChild;

    public State(State parent = null)
    {
        this.parent = parent;
    }

    internal void Attach(StateMachine m) => machine = m;

    // Initial child to enter when this state start (null = this is the leaf)
    protected virtual State GetInitialState() => null;
    
    // Target state to switch to this frame (null = stay in current state)
    protected virtual State GetTransition() => null;
    
    // LifeCycle hooks
    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
    protected virtual void OnUpdate(float deltaTime) { }

    internal void Enter()
    {
        if (parent != null) parent.activeChild = this;
        OnEnter();
        State init = GetInitialState();
        if (init != null) init.Enter();
    }
    internal void Exit()
    {
        if (activeChild != null) activeChild.Exit();
        activeChild = null;
        OnExit();
    }
    internal void Update(float deltaTime)
    {
        State t = GetTransition();
        if (t != null)
        {
            machine.ChangeState(this, t);
            return;
        }

        if (activeChild != null)
        {
            activeChild.Update(deltaTime);
            return;
        }
        
        // OnUpdate only in Leaf Node
        OnUpdate(deltaTime);
    }
    
    // returns the deepest current active descendant state (the leaf of the active path)
    public State Leaf()
    {
        State s = this;
        while (s.activeChild != null) s = s.activeChild;
        return s;
    }
    
    // Yields this state and then ancestor up to the root (self -> parent -> ... -> root)
    public IEnumerable<State> PathToRoot()
    {
        for (State s = this; s != null; s = s.parent) yield return s;
    }
}