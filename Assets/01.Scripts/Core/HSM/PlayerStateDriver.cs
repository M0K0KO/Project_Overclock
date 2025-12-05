using System;
using System.Linq;
using UnityEngine;

public class PlayerStateDriver : MonoBehaviour
{
    private Actor actor;
    private ActorContext context;

    private string lastPath;

    private StateMachine machine;
    private State root;
    
    private void Start()
    {
        actor = GetComponent<Actor>();
        context = actor.actorContext;
        
        PlayerStatesRegistry.Clear();
        
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
            Logwin.Log("PlayerState", path, "Player", LogwinParam.Color(Color.cyan));
            lastPath = path;
        }
    }

    static string StatePath(State s)
    {
        return string.Join(" > ", s.PathToRoot().AsEnumerable().Reverse().Select(n => n.GetType().Name.Replace("Player", "")));
    }
}