using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputActionAsset inputActions;
    public CharacterController playerCharacterController;
    public PlayerStats stats;
    public Animator playerAnim;

    private InputAction playerMoveAction;
    private InputAction playerJumpAction;
    private InputAction playerLookAction;
    private InputAction playerCursorLock;
    private InputAction playerSprintAction;

    [SerializeField] public Transform player;
    [SerializeField] public Transform playerHead;

    private float mouseSensitivity = .24f;
    private float xYaw;
    private float yPitch;

    private Vector3 move;
    private Vector3 jump;
    private Vector3 finalMove;

    private Vector2 playerMoveAmount;
    private float verticalVelocity;
    private float buttonHeldFor;
    private float pressTime;

    private bool sprintIsPressed;
    private float adjustedWalkSpeed;


    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        playerJumpAction.performed += Jump;
        playerCursorLock.performed += OnLock;
        playerSprintAction.started += Sprint;
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
        playerJumpAction.performed -= Jump;
        playerCursorLock.performed -= OnLock;
        playerSprintAction.started -= Sprint;
    }

    private void Awake()
    {
        playerMoveAction = inputActions.FindAction("Move");
        playerJumpAction = inputActions.FindAction("Jump");
        playerCursorLock = inputActions.FindAction("CursorLock");
        playerSprintAction = inputActions.FindAction("Sprint");

        adjustedWalkSpeed = stats.walkSpeed;
    }

    private void Update()
    {
        PlayerPhysics();
    }

    private void FixedUpdate()
    {

        LookAroundPlayer();
        if (!playerCharacterController.isGrounded) {
            verticalVelocity += stats.playerGravity * Time.deltaTime * 2;
        }
    }

    private void PlayerPhysics()
    {
        playerMoveAmount = playerMoveAction.ReadValue<Vector2>();
        move = transform.right * playerMoveAmount.x + transform.forward * playerMoveAmount.y;

        jump = new Vector3(0f, verticalVelocity, 0f);
        finalMove = (move * adjustedWalkSpeed) + jump;
        playerCharacterController.Move(finalMove * Time.deltaTime);
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

    private void Sprint(InputAction.CallbackContext context)
    {
        if (sprintIsPressed == true) {
            sprintIsPressed = false;
            adjustedWalkSpeed = stats.walkSpeed;
        } else if (sprintIsPressed == false) {
            sprintIsPressed = true;
            adjustedWalkSpeed += 2f;
        }
        playerAnim.SetBool("sprintKeyPressed", sprintIsPressed);
    }
    

    
}
