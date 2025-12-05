using System;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer
{
    private readonly float bufferTime;
    private readonly Queue<PlayerCommand> queue = new();

    public InputBuffer(float bufferTime)
    {
        this.bufferTime = bufferTime;
    }

    public void Enqueue(PlayerCommandType type, float time)
    {
        queue.Enqueue(new PlayerCommand(type, time));
    }

    void PruneExpiredCommands(float now)
    {
        while (queue.Count > 0)
        {
            var command = queue.Peek();
            if (now - command.time > bufferTime)
                queue.Dequeue();
            else
                break;
        }
    }

    public bool TryConsume(PlayerCommandType type, float now)
    {
        PruneExpiredCommands(now);
        if (queue.Count == 0) return false;
        
        var command = queue.Peek();
        if (command.type != type) return false;
        
        queue.Dequeue();
        return true;
    }

    public void Clear()
    {
        queue.Clear();
    }
    
#if UNITY_EDITOR
    public void ForEachDebug(float now, Action<PlayerCommand, float> visitor)
    {
        PruneExpiredCommands(now);
        foreach (var command in queue)
        {
            float age = now - command.time;
            visitor(command, age);
        }
    }
#endif
}
