using UnityEngine;

public class Player : MonoBehaviour {
    // * <Movement>

    public float speed = 5f;
    public float JumpForce = 5f;

    private float moveInput;

    public int _ExtraJumps = 1;
    private int ExtraJumps = 1;

        // * <Bools>

        private bool isGrounded;
        private bool _isGrounded;
        private bool canMove = true;
        private bool isJumping = false;

        // * </Bools>

    // * </Movement>

    // * <rb and anim>

    private Rigidbody2D rb;
    private Animator anim;

    // * </rb and anim>

    // * </Knockback>

    private bool isKnokedback = false;
    private bool canKnokedback = true;

    [SerializeField] private Vector2 KnockDir;

    // * </Knockback>

    // * <WallSliding>

    private bool wasWallSliding = false;
    private bool canWallSlide = false;
    private bool isWallSliding = false;

    // * </WallSliding>

    // * <other>

    private bool isWallDetected;
    private bool isFacingRight = true;
    private int FacingDir = 1;

    [SerializeField] private float coyoteTime = .2f;

    // * </other>

    // * <CollisionChecks>

        // * <Ground>

        [SerializeField] private Transform GroundCheck;
        [SerializeField] private LayerMask Ground;

        // * </Ground>

        // * <Wall>

        [SerializeField] private Transform WallCheck;

        // * </Wall>

        // * <other>

        [SerializeField] private float CheckRadious = .3f;

        // * </other>

    // * <CollisionChecks>


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();

        ExtraJumps = _ExtraJumps;
    }

    private void Update() {
        // AnimController();

        if (isKnokedback)
            return;

        TakeInput();
        CollisionCheck();

        if (isWallDetected && rb.velocity.y < 0) {
            canWallSlide = true;
        } else {
            canWallSlide = false;
        }

        Move();
        Flipper();
    }

    private void FixedUpdate() {
        if (!isWallSliding && canMove) {
            rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
        }
    }

    private void TakeInput() {
        moveInput = Input.GetAxis("Horizontal");
        isJumping = Input.GetKeyDown("z");

        isWallSliding = canWallSlide && Input.GetKey("x");
    }

    private void CollisionCheck() {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadious, Ground);
        isWallDetected = Physics2D.OverlapCircle(WallCheck.position, CheckRadious, Ground);

        if (_isGrounded) {
            isGrounded = true;
            ExtraJumps = _ExtraJumps;
        } else {
            Invoke("UnGround", coyoteTime);
        }
    }

    private void Move() {
        if (isWallSliding) {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * .1f);
            wasWallSliding = true;
        } else {
            Invoke("UnWallSliding", .2f);
        }

        if (isJumping) {
            JumpManager();
        }
    }
    private void UnWallSliding() {
        wasWallSliding = false;
    }

    // * Jump

    private void JumpManager() {
        if (wasWallSliding) {
            WallJump();
        } else {
            if (isGrounded) {
                Jump();
            } else if (ExtraJumps > 0) {
                Jump();
                ExtraJumps -= 1;
            }
        }

        canWallSlide = false;
    }

    private void WallJump() {
        canMove = false;
        rb.velocity = new Vector2 (5 * -FacingDir, JumpForce);
        ExtraJumps = _ExtraJumps;

        Invoke("ResetCanMove", .2f);
    }

    private void Jump() {
        rb.velocity = new Vector2 (rb.velocity.x, (Vector2.up * JumpForce).y);
    }


    // * Anims

    private void AnimController() {
        if (moveInput != 0) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        anim.SetFloat("Y_velocity", Mathf.Clamp(rb.velocity.y, -1, 1));
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isGrounded", _isGrounded);
        anim.SetBool("isKnokedback", isKnokedback);
    }

    // * Flipping

    private void Flipper() {
        if (isFacingRight && moveInput < 0) {
            Flip();
        } else if (!isFacingRight && moveInput > 0) {
            Flip();
        }
    }

    private void Flip() {
        if (!isWallSliding) {
            FacingDir *= -1;

            transform.Rotate(0, 180, 0);
            isFacingRight = !isFacingRight;
        }
    }

    // * Resets

    private void UnGround() {
        isGrounded = false;
    }


    private void ResetCanMove() {
        canMove = true;
    }

    private void UnKnockBack() {
        isKnokedback = false;
    }

    private void ResetCanKnockBack() {
        canKnokedback = true;
    }

    // * KnockBack

    public void Knockback(int dir) {
        if (canKnokedback) {
            canMove = false;
            isKnokedback = true;
            canKnokedback = false;

            rb.velocity = new Vector2 (KnockDir.x * dir, KnockDir.y);

            Invoke("ResetCanMove", .34f);
            Invoke("UnKnockBack", .34f);
            Invoke("ResetCanKnockBack", 1.5f);
        }
    }
}
