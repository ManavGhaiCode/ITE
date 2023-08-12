using UnityEngine;

public class WalkingEnemy : Enemy {
    private int facingDir = 1;
    private bool isFacingRight = true;

    private bool isWallDetected;

    [SerializeField] private float WallCheckDistace;
    [SerializeField] private LayerMask Ground;

    private void Update() {
        isWallDetected = Physics2D.Raycast(transform.position, new Vector2 (facingDir, 0), WallCheckDistace, Ground);

        if (isWallDetected) {
            if (isFacingRight) {
                facingDir = -1;
            } else {
                facingDir = 1;
            }

            isFacingRight = !isFacingRight;
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2 (speed * facingDir, rb.velocity.y);
    }
}