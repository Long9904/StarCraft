using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f; // Ensure the game runs at normal speed when starting a new game
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
