using UnityEngine;

public class AI_PauseManager : MonoBehaviour
{
    public GameObject gameCanvas; // Canvas for game UI
    public GameObject menuCanvas; // Canvas for pause menu

    private bool isPaused = false;

    private void Start()
    {
        // Hide the menu canvas initially and ensure game canvas is visible
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            // Resume game
            ResumeGame();
        }
        else
        {
            // Pause game
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Pause the game
        gameCanvas.SetActive(false); // Hide game canvas
        menuCanvas.SetActive(true); // Show menu canvas
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Resume the game
        gameCanvas.SetActive(true); // Show game canvas
        menuCanvas.SetActive(false); // Hide menu canvas
    }

    public void GoToSettings()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); 
    }

    public void Reset()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2); 
    }
}
