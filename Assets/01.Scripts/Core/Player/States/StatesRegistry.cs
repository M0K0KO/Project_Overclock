using System.Collections.Generic;
using UnityEngine;

public abstract class StatesRegistry
{
    protected readonly Dictionary<PlayerStateID, State> idToState = new();
    protected readonly Dictionary<State, PlayerStateID> stateToID = new();

    public abstract void Clear();

    public abstract void Register(PlayerStateID id, State state);

    public abstract State Get(PlayerStateID id);

    public abstract bool TryGetID(State state, out PlayerStateID id);
}

public static class PlayerStatesRegistry
{
    static readonly Dictionary<PlayerStateID, State> idToState = new();
    static readonly Dictionary<State, PlayerStateID> stateToId = new();

    public static void Clear()
    {
        idToState.Clear();
        stateToId.Clear();
    }

    public static void Register(PlayerStateID id, State state)
    {
        if (state == null) return;

        idToState[id] = state;
        stateToId[state] = id;
    }

    public static State Get(PlayerStateID id)
    {
        idToState.TryGetValue(id, out var s);
        return s;
    }

    public static bool TryGetId(State state, out PlayerStateID id)
        => stateToId.TryGetValue(state, out id);
}

