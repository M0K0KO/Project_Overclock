using System.Collections.Generic;
using System.Reflection;

public class StateMachineBuilder
{
    private readonly State root;

    public StateMachineBuilder(State root)
    {
        this.root = root;
    }

    public StateMachine Build()
    {
        var m = new StateMachine(root);
        Wire(root, m, new HashSet<State>());
        return m;
    }

    void Wire(State s, StateMachine m, HashSet<State> visited)
    {
        if (s == null) return;
        if (!visited.Add(s)) return;


        var flags = BindingFlags.Instance 
                    | BindingFlags.NonPublic 
                    | BindingFlags.Public 
                    | BindingFlags.FlattenHierarchy;
        var machineField = typeof(State).GetField("machine", flags);
        if (machineField != null) machineField.SetValue(s, m);
        s.Attach(m);

        foreach (var field in s.GetType().GetFields(flags))
        {
            if (!typeof(State).IsAssignableFrom(field.FieldType)) continue;
            if (field.Name == "Parent") continue;
            
            var child = (State)field.GetValue(s);
            if (child == null) continue;
            if (!ReferenceEquals(child.parent, s)) continue;
            
            Wire(child, m, visited);
        }
    }
}