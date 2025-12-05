using UnityEngine;

public class MovementContext
{
    public Vector3 velocity;
    public float horizontalVelocity => new Vector2(velocity.x, velocity.z).magnitude;
    public float verticalVelocity => velocity.y;
    
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justJumped;

    public float lastGroundedTime;
}


