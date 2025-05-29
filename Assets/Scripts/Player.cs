using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("UI Manager")]
    [SerializeField] HeartManager heartManager;  // Referência ao gerenciador de corações

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        heartManager.UpdateHearts(currentHealth);
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
            GameManager manager = gameManager.GetComponent<GameManager>();
            manager.EndGame();
        }
    }

}
