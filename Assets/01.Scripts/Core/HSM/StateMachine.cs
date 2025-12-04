using System.Collections.Generic;

public class StateMachine
{
    public readonly State root;
    private bool started;
    
    private Stack<State> st = new Stack<State>();

    public StateMachine(State root)
    {
        this.root = root;
    }

    public void ChangeState(State from, State to)
    {
        if (from == to || from == null || to == null) return;
        
        State lca = LowestCommonAncestor(from, to);

        // exit current branch up to LCA
        for (State s = from; s != lca; s = s.parent)
            s.Exit();

        st.Clear();
        
        // enter target branch from lca down to target
        for (State s = to; s != lca; s=s.parent) 
            st.Push(s);
        while (st.Count > 0) st.Pop().Enter();
    }

    public void Start()
    {
        if (started) return;
        
        started = true;
        root.Enter();
    }

    public void Tick(float deltaTime)
    {
        if (!started) Start();
        InternalTick(deltaTime);
    }
    internal void InternalTick(float deltaTime) => root.Update(deltaTime);

    public static State LowestCommonAncestor(State from, State to)
    {
        var fromParent = new HashSet<State>();
        for (State s = from; s != null; s = s.parent)
            fromParent.Add(s);
        
        for (State s = to; s != null; s = s.parent)
            if (fromParent.Contains(s))
                return s;

        return null;
    }
}