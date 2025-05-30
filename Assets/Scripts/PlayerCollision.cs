using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Spawner spawner;

    private PlayerMovement playerMovement;

    void Awake()
    {
        spawner = FindAnyObjectByType<Spawner>();

        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        EnemyRanged enemyRanged = collision.gameObject.GetComponent<EnemyRanged>();
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (enemy != null)
        {
            if (playerMovement.isDashing)
            {
                Debug.Log("Enemy damaged enemy while dashing");
                Destroy(enemy.gameObject);
                spawner.RemoveEnemy(collision.gameObject);
            }
            else if (!playerMovement.isDashing && enemy.isDashing)
            {
                Debug.Log("Player damaged player while dashing");
                GetComponent<Player>().TakeDamage();
            }
        }

        if (enemyRanged != null)
        {
            if (playerMovement.isDashing)
            {
                Destroy(enemyRanged.gameObject);
                spawner.RemoveEnemy(collision.gameObject);
            }
        }

        if (projectile != null)
        {
            if (playerMovement.isDashing)
            {
                Destroy(projectile.gameObject);
            }
            else if (!playerMovement.isDashing)
            {
                gameObject.GetComponent<Player>().TakeDamage();
            }

        }
    }
}
