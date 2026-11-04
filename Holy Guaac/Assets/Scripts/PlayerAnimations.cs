using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    public CharacterController controller;
    public Animator playerAnim;

    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float verticalSpeed;

    [SerializeField] public bool jumping;
    [SerializeField] public bool falling;
    [SerializeField] public bool moving;

    public Vector3 horizontalVelocity;
    public Vector3 verticalVelocity;

    private float velocityThreshold = 0.1f;

    void FixedUpdate()
    {
        horizontalVelocity = new Vector3(controller.velocity.x, 0f, controller.velocity.z);
        verticalVelocity = new Vector3(0f, controller.velocity.y, 0f);

        horizontalSpeed = horizontalVelocity.magnitude;
        if (horizontalSpeed < velocityThreshold) {
            horizontalSpeed = 0f;
        }
        
        if (verticalVelocity.y > 0.1f)
        {
            verticalSpeed = verticalVelocity.magnitude;
        } else if (verticalVelocity.y < -0.1f)
        {
            verticalSpeed = verticalVelocity.magnitude * -1f;
        }

        JumpCheck();
        MoveCheck();
    }

    private void JumpCheck()
    {
        if (verticalSpeed > velocityThreshold && !controller.isGrounded) {
            jumping = true;
            falling = false;
        } else if (verticalSpeed < velocityThreshold && !controller.isGrounded) {
            falling = true;
            jumping = false;
        } else if (controller.isGrounded) {
            jumping = false;
            falling = false;
            verticalSpeed = 0f;
        }

        playerAnim.SetBool("jumping", jumping);
        playerAnim.SetBool("falling", falling);
    }

    private void MoveCheck()
    {
        if (horizontalSpeed > velocityThreshold)
        {
            moving = true;
        } else if (horizontalSpeed < velocityThreshold)
        {
            moving = false;
        }

        playerAnim.SetBool("moving", moving);
    }
}
