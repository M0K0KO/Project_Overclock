using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private InputAction movementAction;
    private InputAction jumpAction;

    public Vector2 moveInput { get; private set; } = Vector2.zero;


    [SerializeField] private float jumpBufferTime = 0.15f;
    public InputBuffer commandBuffer { get; private set; }
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        movementAction = playerInputActions.Gameplay.Movement;
        jumpAction = playerInputActions.Gameplay.Jump;

        commandBuffer = new InputBuffer(jumpBufferTime);
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
        commandBuffer.Enqueue(PlayerCommandType.Jump, Time.time);
    }

    #endregion
}
