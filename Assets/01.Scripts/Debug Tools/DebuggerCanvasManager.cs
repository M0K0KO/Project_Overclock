#if UNITY_EDITOR

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DebuggerCanvasManager : MonoBehaviour
{
    [Header("Debug Usages")]
    [SerializeField] private bool useInputBufferVisualizer = true;

    [Header("Input Buffer Visualizer")]
    [SerializeField] private InputBufferVisualizer inputBufferVisualizer;

    [SerializeField] private GameObject inputBufferVisualRect;
    [SerializeField] private TextMeshProUGUI inputBufferText;


    private void OnValidate()
    {
        inputBufferVisualRect.SetActive(useInputBufferVisualizer);
    }

    private void LateUpdate()
    {
        if (useInputBufferVisualizer)
        {
            var sb = new StringBuilder();

            List<string> lines = inputBufferVisualizer.GetInputBuffer();

            sb.AppendLine("Input Buffer\n");
            foreach (string line in lines)
            {
                sb.AppendLine(line);
            }

            inputBufferText.text = sb.ToString();
        }
    }
}

#endif