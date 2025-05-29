using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart Sprites")]
    public Sprite fullHeart;  // Sprite for a full (active) heart
    public Sprite emptyHeart; // Sprite for an empty (inactive) heart

    [Header("Heart UI Elements")]
    public Image[] hearts; // Array of Image components representing heart slots in the UI

    // Updates the heart images based on the player's current health
    public void UpdateHearts(int currentHealth)
    {
        // Loop through all heart slots
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                // If index is less than current health, show full heart
                hearts[i].sprite = fullHeart;
            }
            else
            {
                // Otherwise, show empty heart
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
