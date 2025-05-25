using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private int damageToPlayer = 1;

    private bool isDead = false;

    private Transform player;
    private Rigidbody2D rb;
    public bool isDashing { get; private set; }
    private float lastDashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= dashDistance && Time.time > lastDashTime + dashCooldown)
        {
            StartCoroutine(DashToPlayer());
        }
        else if (!isDashing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    private IEnumerator DashToPlayer()
    {
        isDashing = true;
        lastDashTime = Time.time;

        Vector2 dashDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(0.5f);
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (isDashing)
            {
                Debug.Log("Enemy damaged player while dashing!");
            }
            else if (player.isDashing)
            {
                Debug.Log("Player dashed into enemy! Enemy dies.");
                Destroy(gameObject);
                isDead = true;
            }
        }
    }
}
