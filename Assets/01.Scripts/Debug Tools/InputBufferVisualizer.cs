#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

public class InputBufferVisualizer : MonoBehaviour
{
    [SerializeField] private PlayerInputAdapter inputAdapter;
    readonly List<string> lines = new (16);

    public List<string> GetInputBuffer()
    {
        if (inputAdapter == null) return null;

        float now = Time.time;
        lines.Clear();

        lines.Add($"Move: {inputAdapter.moveInput.x:F2}, {inputAdapter.moveInput.y:F2}");

        inputAdapter.commandBuffer.ForEachDebug(now, (cmd, age) =>
        {
            lines.Add($"  - {cmd.type} ({age:0.000}s ago)");
        });

        return lines;
    }
}

#endif
