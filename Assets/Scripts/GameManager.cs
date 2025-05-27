using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
