using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private Vector2 mousePosition;
    public bool canMove = true;
    public float moveSpeed = 10f;

    public float followMaxDistance = 1f;

    bool wantsToDash = false;
    public float dashDistance = 5f;
    public float dashCooldown = 0.5f;
    private float lastDashTime;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            wantsToDash = true;
        }
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            MovementFollowMouse();
        }

        if (wantsToDash)
        {
            wantsToDash = false;
            StartCoroutine(PerformDash());
        }

    }

    IEnumerator PerformDash()
    {
            canMove = false;
            Dash();
            Debug.Log("Dash");

            yield return new WaitForSeconds(dashCooldown);
            canMove = true;
    }

    void MovementFollowMouse()
    {
        //Player seguindo o mouse
        // Pega a posição do mouse na tela e converte para o mundo
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcula a distância entre o player e o mouse
        float distanceToMouse = Vector2.Distance(transform.position, mousePosition);

        if (distanceToMouse >= followMaxDistance)
        {
            // Player segue o mouse sempre na mesma velocidade
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);

        }



        //Sprite apontar para onde está andando
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    
    }


    void Dash()

    {

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mouseWorldPosition - (Vector2)transform.position).normalized;
        Vector2 dashTarget = (Vector2)transform.position + dashDirection * dashDistance;

        rb.MovePosition(dashTarget); // Move instantaneamente
    }
}
