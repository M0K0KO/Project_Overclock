using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private InputAction movementAction;
    private InputAction jumpAction;

    public Vector2 moveInput { get; private set; } = Vector2.zero;
    
    public bool jumpRequested { get; private set; } = false;
    public float lastJumpRequestTime { get; private set; }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        movementAction = playerInputActions.Gameplay.Movement;
        jumpAction = playerInputActions.Gameplay.Jump;
    }

    private void OnEnable()
    {
        if (playerInputActions == null) playerInputActions = new PlayerInputActions();

        movementAction.performed += OnMovement;
        movementAction.canceled += OnMovement;

        jumpAction.performed += OnJump;
        
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMovement;
        movementAction.canceled -= OnMovement;
        
        jumpAction.performed -= OnJump;

        playerInputActions.Disable();
    }

    #region CallBacks

    private void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpRequested = true;
        lastJumpRequestTime = Time.time;
    }

    #endregion

    #region Consumes

    public void ConsumeJumpInput()
    {
        jumpRequested = false;
    }

    #endregion
}
