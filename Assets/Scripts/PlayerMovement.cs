using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private Vector2 mousePosition;
    public bool canMove = true;
    public float moveSpeed = 10f;

    public float followMaxDistance = 1f;


    public float dashDistance = 50f;
    public float dashCooldown = 1f;
    private float lastDashTime;



    void FixedUpdate()
    {
        MovementFollowMouse();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
            Debug.Log("Dash");
        }
    }

    void MovementFollowMouse()
    {
        if (canMove)
        {
            //Player seguindo o mouse
            // Pega a posição do mouse na tela e converte para o mundo
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Calcula a distância entre o player e o mouse
            float distanceToMouse = Vector2.Distance(transform.position, mousePosition);

            if (distanceToMouse >= followMaxDistance)
            {
                // Player segue o mouse suavemente
                // transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);

                // Player segue o mouse sempre na mesma velocidade
                Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
                rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);

            }



            //Sprite apontar para onde está andando
            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }


    void Dash()

    {

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mouseWorldPosition - (Vector2)transform.position).normalized;
        Vector2 dashTarget = (Vector2)transform.position + dashDirection * dashDistance;

        rb.MovePosition(dashTarget); // Move instantaneamente
    }
}
