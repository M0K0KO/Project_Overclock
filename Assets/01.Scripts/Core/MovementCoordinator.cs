using System;
using MiniTools.BetterGizmos;
using UnityEngine;

public interface IMovementSource
{
    public Vector3 CalculateVelocity();
}

public class MovementCoordinator : MonoBehaviour
{
    private IMovementSource[] movementSources;
    private Actor actor;
    private ActorContext actorContext;
    private MovementContext movementContext;
    
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Debug")] 
    [SerializeField] private bool drawIsGrounded = true;
    
    // temporarily moved to Start Because of the initialization order issue;
    private void Start()
    {
        movementSources = GetComponents<IMovementSource>();
        
        actor = GetComponent<Actor>();
        actorContext = actor.actorContext;
        movementContext = actorContext.movementContext;
    }

    private void Update()
    {
        Vector3 velocity = Vector3.zero;
        foreach (IMovementSource movementSource in movementSources)
        {
            velocity += movementSource.CalculateVelocity();
        }
        movementContext.velocity = velocity;
        actorContext.cc.Move(velocity * Time.deltaTime);
        bool isGrounded = CheckIsGrounded(actorContext, groundCheckDistance, groundMask);
        if (movementContext.justJumped)
        {
            movementContext.isGrounded = false;
            movementContext.justJumped = false;
        }
        else
        {
            movementContext.isGrounded = isGrounded;
        }

        if (isGrounded)
        {
            movementContext.lastGroundedTime = Time.time;
        }

#if UNITY_EDITOR
        Logwin.Log("Player isGrounded", movementContext.isGrounded, "Player");
        Logwin.Log("Player VerticalVelocity", movementContext.verticalVelocity, "Player");
        Logwin.Log("Player HorizontalVelocity", movementContext.horizontalVelocity, "Player");
#endif
        
        
        UpdateRotation(Time.deltaTime);
    }

    private void LateUpdate()
    {
        MovementVectorGizmoDrawer.instance.DrawVelocityGizmo(actorContext.transform.position + Vector3.up * 0.5f, movementContext.velocity, Color.red, Color.yellow);
    }

    public bool CheckIsGrounded(ActorContext ctx, float extraDist, LayerMask mask)
    {
        Vector3 origin = ctx.transform.position;
        float radius = ctx.cc.radius * 0.95f;
        float castDistance = (ctx.cc.height / 2f) + extraDist;
        
        bool isGrounded = Physics.SphereCast(origin, radius, Vector3.down, out RaycastHit hit, castDistance, mask);
        UpdateWasGroundedLastFrame(isGrounded);
        
        return isGrounded;
    }

    private void UpdateWasGroundedLastFrame(bool isGroundedThisFrame)
    {
#if UNITY_EDITOR
        if (!movementContext.wasGroundedLastFrame && isGroundedThisFrame)
        {
            Logwin.Log("Player Landing", "Played Landed!", "Player");
        }
#endif
        
        movementContext.wasGroundedLastFrame = isGroundedThisFrame;
    }

    void UpdateRotation(float deltaTime)
    {
        Vector3 currentForward = transform.forward;
        Vector3 targetForward = currentForward;

        switch (movementContext.facingMode)
        {
            case FacingMode.Free :
                if (movementContext.horizontalVelocity > 0f)
                {
                    targetForward = movementContext.velocity;
                }
                break;
        }

        targetForward.y = 0f;
        if (targetForward.sqrMagnitude < Mathf.Epsilon) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetForward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f - Mathf.Exp(-rotationSpeed * deltaTime));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (drawIsGrounded)
                BetterGizmos.Spherecast(
                    Color.red,
                    Color.green,
                    0.2f,
                    actorContext.transform.position,
                    actorContext.cc.radius * 0.95f,
                    Vector3.down,
                    (actorContext.cc.height / 2f) + groundCheckDistance,
                    groundMask);
        }
    }
#endif
}