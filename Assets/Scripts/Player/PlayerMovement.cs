using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private float horizontalInput;
    private bool jumpPressed;
    private float wallJumpCooldown;

    private Animator anim;
    private BoxCollider2D boxCollider;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        // Get references to components from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Use Update for input handling because it runs every frame and can detect input changes immediately
    private void Update()
    {
        horizontalInput = 0f;

        if (Keyboard.current.aKey.isPressed)
            horizontalInput = -1f;
        else if (Keyboard.current.dKey.isPressed)
            horizontalInput = 1f;

        // Flip player when moving left and right
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1.2f, 1.2f, 1);

        // Set animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            jumpPressed = true;

        // Adjustable jump height
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);

        // Coyote time
        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
            coyoteCounter -= Time.deltaTime;
    }

    // Use FixedUpdate for physics-based movement because it works in time intervals regardless of frame rate
    private void FixedUpdate()
    {
        if (wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
            }
        }
        else
        {
            wallJumpCooldown += Time.fixedDeltaTime;
        }

        // Jump logic
        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }
    }

    private void Jump()
    {
        // If coyote time is over, not on a wall, and no extra jumps left, don't jump
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0)
            return;

        SoundManager.instance.PlaySound(jumpSound);

        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY);

            wallJumpCooldown = 0;
        }
        else
        {
            if (coyoteCounter > 0)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            }
            else if (jumpCounter > 0)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                jumpCounter--;
            }
        }

        // Prevent using coyote time twice
        coyoteCounter = 0;
        jumpPressed = false;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycasthit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x, 0f), 0.1f, wallLayer);
        return raycasthit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

}