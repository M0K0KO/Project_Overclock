using UnityEngine;

public enum FacingMode
{
    Free,
}

public enum RootMotionMode
{
    None,
    Locomotion,
    Action,
}

public class MovementContext
{
    public Vector3 velocity;
    public float horizontalVelocity => new Vector2(velocity.x, velocity.z).magnitude;
    public float verticalVelocity => velocity.y;
    public FacingMode facingMode;
    
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justJumped;
    public bool shouldPlayDoubleJumpAnimation;
    public int jumpCount = 0;
    
    public RootMotionMode rootMotionMode;
    public Vector3 rootMotionDeltaPosition;
    public Quaternion rootMotionDeltaRotation;
    public float rootMotionScale;

    public float lastGroundedTime;
}


