using System;
using UnityEngine;

public class VoluntaryMovement : MonoBehaviour, IMovementSource
{
    private PlayerContext playerContext;
    private MovementContext movementContext;
    private PlayerInputAdapter inputAdapter;

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float jumpSpeed = 25f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float gravityAcceleration = -70f;
    [SerializeField] private float defaultGravity = -10f;

    // temporarily moved to Start Because of the initialization order issue;
    private void Start()
    {
        var actor = GetComponent<Actor>();
        var context = actor.actorContext;

        movementContext = context.movementContext;
        inputAdapter = (context as PlayerContext)?.inputAdapter;
        playerContext = context as PlayerContext;
    }

    public Vector3 CalculateVelocity()
    {
        if (!inputAdapter || playerContext == null || !playerContext.playerCam)
            return Vector3.zero;
        
        Vector2 inputDir = inputAdapter.moveInput;
        Vector3 horizontalVelocity = CalculateHorizontalVelocity(inputDir);
        Vector3 verticalVelocity = CalculateVerticalVelocity();
        
        return (horizontalVelocity * moveSpeed) + verticalVelocity;
    }

    private Vector3 CalculateVerticalVelocity()
    {
        float currentYVelocity = movementContext.velocity.y;
        float targetYVelocity = currentYVelocity;

        Logwin.Log("JumpCount", movementContext.jumpCount, "Player");
        
        if ((IsGroundedOrCoyote() || CanDoubleJump()) && inputAdapter.commandBuffer.TryConsume(PlayerCommandType.Jump, Time.time))
        {
            targetYVelocity = jumpSpeed;
            movementContext.jumpCount++;
            movementContext.justJumped = true;

            if (movementContext.jumpCount == 2) movementContext.shouldPlayDoubleJumpAnimation = true;
        }
        else if (movementContext.isGrounded)
        {
            targetYVelocity = defaultGravity;
        }
        else
        {
            targetYVelocity += gravityAcceleration * Time.deltaTime;
        }
        
        return targetYVelocity * Vector3.up;
    }

    private Vector3 CalculateHorizontalVelocity(Vector2 inputDir)
    {
        Vector3 camF = playerContext.playerCam.transform.forward;
        Vector3 camR = playerContext.playerCam.transform.right;
        camF.y = 0;
        camR.y = 0;
        camF.Normalize();
        camR.Normalize();

        Vector3 moveDir = camF * inputDir.y + camR * inputDir.x;
        moveDir.Normalize();
        return moveDir;
    }
    
    private bool IsGroundedOrCoyote() =>
        movementContext.isGrounded || Time.time - movementContext.lastGroundedTime <= coyoteTime;

    private bool CanDoubleJump()
    {
        return movementContext.jumpCount == 1 && !movementContext.isGrounded;
    }
}
