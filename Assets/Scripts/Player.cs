using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] HeartManager heartManager;

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        heartManager.UpdateHearts(currentHealth);

        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void TakeDamage()
    {
        if (currentHealth >= 1)
        {
            currentHealth--;
            heartManager.UpdateHearts(currentHealth);
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
            if (gameManager == null)
            {
                gameManager = FindAnyObjectByType<GameManager>();
            }
            gameManager.EndGame();
        }
    }

}
