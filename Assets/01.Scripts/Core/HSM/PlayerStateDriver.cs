using System;
using System.Linq;
using UnityEngine;

public class PlayerStateDriver : MonoBehaviour
{
    private Actor actor;
    private ActorContext context;
    private MovementContext movementContext;

    private string lastPath;

    private StateMachine machine;
    private State root;

    private void Start()
    {
        actor = GetComponent<Actor>();
        context = actor.actorContext;
        movementContext = context.movementContext;

        root = new PlayerRoot(null, context as PlayerContext);
        var builder = new StateMachineBuilder(root);

        machine = builder.Build(); 
    }

    private void Update()
    {
        machine.Tick(Time.deltaTime);

        var path = StatePath(machine.root.Leaf());
        if (path != lastPath)
        {
            Logwin.Log("State", path, "Player", LogwinParam.Color(Color.cyan));
            lastPath = path;
        }
    }

    static string StatePath(State s)
    {
        return string.Join(" > ", s.PathToRoot().AsEnumerable().Reverse().Select(n => n.GetType().Name));
    }
}