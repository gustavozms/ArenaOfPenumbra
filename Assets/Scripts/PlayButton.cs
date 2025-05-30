using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(Action);
        }
        else
        {
            Debug.LogError("PlayButton script is not attached to a GameObject with a Button component");
        }
    }

    private void Action()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.StartGame();
        }
        else
        {
            Debug.LogError("GameManager not found");
        }
    }
}