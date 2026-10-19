using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputActionAsset inputActions;
    public CharacterController playerCharacterController;
    public PlayerStats stats;

    private InputAction playerMoveAction;
    private InputAction playerJumpAction;
    private InputAction playerLookAction;
    private InputAction playerCursorLock;

    [SerializeField] public Transform player;
    [SerializeField] public Transform playerHead;

    private float mouseSensitivity = .12f;
    private float xYaw;
    private float yPitch;

    private Vector3 move;
    private Vector3 jump;

    private Vector2 playerMoveAmount;
    private float verticalVelocity;
    private float buttonHeldFor;
    private float pressTime;


    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        playerJumpAction.performed += Jump;
        playerCursorLock.performed += OnLock;
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
        playerJumpAction.performed -= Jump;
        playerCursorLock.performed -= OnLock;
    }

    private void Awake()
    {
        playerMoveAction = inputActions.FindAction("Move");
        playerJumpAction = inputActions.FindAction("Jump");
        playerCursorLock = inputActions.FindAction("CursorLock");
    }

    private void Update()
    {
        PlayerPhysics();
        LookAroundPlayer();
            verticalVelocity += stats.playerGravity * Time.deltaTime * 2;

    }

    private void PlayerPhysics()
    {
        Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
        playerMoveAmount = playerMoveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * playerMoveAmount.x + transform.forward * playerMoveAmount.y;

        Vector3 finalMove = move * stats.walkSpeed;
        playerCharacterController.Move(finalMove * Time.deltaTime);

        jump = new Vector3(0f, verticalVelocity, 0f);
        playerCharacterController.Move(jump * Time.deltaTime);

    }

    private void LookAroundPlayer()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        xYaw += mouseDelta.x * mouseSensitivity;
        yPitch -= mouseDelta.y * mouseSensitivity;
        yPitch = Mathf.Clamp(yPitch, -40f, 70f);

        player.transform.rotation = Quaternion.Euler(0f, xYaw, 0f);
        playerHead.transform.rotation = Quaternion.Euler(yPitch, xYaw, 0f);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (playerCharacterController.isGrounded)
        {
            verticalVelocity = stats.jumpHeight;

        }
        
    }

    private void OnLock(InputAction.CallbackContext context)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } 
        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    

    
}
