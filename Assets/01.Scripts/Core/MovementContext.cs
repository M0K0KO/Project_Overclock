using UnityEngine;

public class MovementContext
{
    public Vector3 velocity;
    
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justJumped;

    public float lastGroundedTime;
}
