using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private Rigidbody2D rb;  // Reference to Rigidbody2D for movement
    private Vector2 mousePosition;  // Stores current mouse position
    public bool canMove = true;  // Indicates if the player can move
    public float moveSpeed = 7f;  // Player movement speed
    public float followMaxDistance = 1f; // Minimum distance for the player to follow the mouse

    [Header("Dash Settings")]
    bool wantsToDash = false; // Flag indicating if the player wants to dash
    public float dashCooldown = 0.5f; // Time between dashes
    private float lastDashTime; // Records the last time the dash was used

    public float dashSpeed = 60f;  // Dash speed
    public float dashDuration = 0.1f;  // Dash duration

    public bool isDashing { get; private set; }  // Current dash state

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize Rigidbody2D
    }

    void Update()
    {
        // Detect dash input (left mouse button) and check cooldown
        if (Input.GetMouseButtonDown(0) && canMove && Time.time >= lastDashTime + dashCooldown)
        {
            wantsToDash = true;
            lastDashTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        // Normal movement: follow the mouse if allowed
        if (canMove)
        {
            MovementFollowMouse();
        }

        // Start dash if requested
        if (wantsToDash)
        {
            wantsToDash = false;
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash()
    {
        isDashing = true;   // Set dash state to true
        canMove = false;    // Prevent normal movement during dash

        Dash(); // Execute the dash

        Debug.Log("Dash");

        yield return new WaitForSeconds(dashDuration);  // Wait for the dash duration
        isDashing = false;  // End dash state
        canMove = true;     // Allow normal movement again
    }

    void MovementFollowMouse()
    {
        // Player follows the mouse

        // Get the mouse position on the screen and convert to world position
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate distance between player and mouse
        float distanceToMouse = Vector2.Distance(transform.position, mousePosition);

        if (distanceToMouse >= followMaxDistance)
        {
            // If far enough, move towards the mouse
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
        }
        else if (distanceToMouse < followMaxDistance)
        {
            // If close enough, stop any residual movement
            rb.linearVelocity = Vector2.zero;
        }

        // Rotate the sprite to face the movement direction
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    void Dash()
    {
        // Calculate dash direction based on current mouse position
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mouseWorldPosition - (Vector2)transform.position).normalized;

        // Apply instant velocity for the dash
        rb.linearVelocity = dashDirection * dashSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect collision with enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // If player is dashing and enemy is not, destroy the enemy
            if (isDashing && !enemy.isDashing)
            {
                Debug.Log("player damaged enemy while dashing");
                Destroy(enemy.gameObject);
            }
        }
    }
}
