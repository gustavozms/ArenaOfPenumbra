using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private Vector2 mousePosition;
    public bool canMove = true;
    public float moveSpeed = 5f;
    public float distanceDash = 10f;

    void Update()
    {
        movementFollowMouse();
    }

    void movementFollowMouse()
    {
        if (canMove)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }

    //void dashIntoMouse()
    //{
    //    if (Input.GetMouseButtonDown(2))
    //    {
    //        canMove = false;
    //        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * 5 * Time.deltaTime);
    //    }
    //}

}
