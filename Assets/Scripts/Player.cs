using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        if (currentHealth >= 1)
        {
            currentHealth--;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            GameManager manager = gameManager.GetComponent<GameManager>();
            manager.EndGame();
        }
    }

}
