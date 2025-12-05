#if UNITY_EDITOR

using MiniTools.BetterGizmos;
using UnityEngine;

public class MovementVectorGizmoDrawer : MonoBehaviour
{
    public static MovementVectorGizmoDrawer instance;

    private const float arrowHeadLength = 0.25f;
    private const float arrowHeadAngle = 30.0f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void DrawVelocityGizmo(Vector3 origin, Vector3 velocity, Color horizontalColor, Color verticalColor)
    {
        Vector3 verticalVelocity = new Vector3(0f, velocity.y, 0f);
        DrawArrowGizmo(origin, verticalVelocity / 10f, verticalColor);
        
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);
        DrawArrowGizmo(origin, horizontalVelocity / 10f, horizontalColor);
    }

    public void DrawArrowGizmo(Vector3 origin, Vector3 destination, Color color, bool normalize = false)
    {
        if (destination == Vector3.zero) return;

        if (normalize) destination.Normalize();

        Debug.DrawLine(origin, origin + destination, color, 0f, false);

        Vector3 tip = origin + destination;
        Quaternion currentRotation = Quaternion.LookRotation(destination);
        Vector3 rightWingDir = currentRotation * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
        Vector3 leftWingDir = currentRotation * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;

        Debug.DrawLine(tip, tip + rightWingDir * arrowHeadLength, color, 0f, false);
        Debug.DrawLine(tip, tip + leftWingDir * arrowHeadLength, color, 0f, false);
    }
}

#endif