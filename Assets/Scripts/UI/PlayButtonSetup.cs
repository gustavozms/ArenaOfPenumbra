using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSetup : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnClicked);
        }
    }

    private void OnClicked()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.StartGame();
        }
    }
}