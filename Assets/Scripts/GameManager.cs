using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
