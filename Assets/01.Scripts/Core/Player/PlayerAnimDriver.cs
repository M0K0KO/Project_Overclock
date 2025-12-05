using System;
using UnityEngine;

public class PlayerAnimDriver : MonoBehaviour
{
    private Animator animator;
    private MovementContext movementContext;

    static readonly int horizontalMoveAmount_Param      = Animator.StringToHash("HorizontalMoveAmount");
    static readonly int verticalMoveAmount_Param      = Animator.StringToHash("VerticalMoveAmount");
    static readonly int speed_Param      = Animator.StringToHash("Speed");
    static readonly int isGrounded_Param = Animator.StringToHash("IsGrounded");
    static readonly int jumpCount_Param = Animator.StringToHash("JumpCount");
    
    private void Start()
    {
        var actor = GetComponent<Actor>();
        animator = actor.actorContext.animator;
        movementContext = actor.actorContext.movementContext;
        
        SetRootMotionMode(RootMotionMode.Locomotion);
    }

    private void OnAnimatorMove()
    {
        if (movementContext.rootMotionMode == RootMotionMode.None)
        {
            movementContext.rootMotionDeltaPosition = Vector3.zero;
            movementContext.rootMotionDeltaRotation = Quaternion.identity;
        }   
        
        movementContext.rootMotionDeltaPosition = animator.deltaPosition * movementContext.rootMotionScale;
        movementContext.rootMotionDeltaRotation = animator.deltaRotation;
    }

    public void UpdateLocomotionParams(Vector3 velocity, bool isGrounded, int jumpCount)
    {
        animator.SetFloat(horizontalMoveAmount_Param, new Vector3(velocity.x, 0, velocity.z).magnitude);
        animator.SetFloat(verticalMoveAmount_Param, velocity.y);
        animator.SetFloat(speed_Param, velocity.magnitude);
        animator.SetBool(isGrounded_Param, isGrounded);
        animator.SetFloat(jumpCount_Param, jumpCount);
    }

    public void PlayLocomotion(string stateName, float fadeTime = 0.05f)
    {
        animator.CrossFadeInFixedTime(stateName, fadeTime);
    }
    
    public void PlayAction(string stateName, float fadeTime = 0.05f)
    {
        animator.CrossFadeInFixedTime(stateName, fadeTime);
    }
    

    public void SetRootMotionMode(RootMotionMode mode, float scale = 1f)
    {
        movementContext.rootMotionMode = mode;
        movementContext.rootMotionScale = scale;
    }
    
    public float GetNormalizedTime(int layer)
    {
        var info = animator.GetCurrentAnimatorStateInfo(layer);
        return info.normalizedTime;
    }
}
