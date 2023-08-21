using UnityEngine;
using UnityEngine.SceneManagement;

//menu manager script
//responsibility: changing gamestate on click of menu buttons. pausing, resuming, resetting and toggling timer.
public class MenuScript : MonoBehaviour, IMenuManager
{
    public GameObject menuPanel;
    public GameObject UiManager;
    public GameStateScript gameState;
    private GameManagerScript gameManager;

    private void Update()
    {
        // Pause the game and activate the pause menu if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !gameState.isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameState.isPaused)
        {
            ResumeGame();
        }
    }

    private void Start()
    {
        // Disable the pause menu at game start
        menuPanel.SetActive(false);
        gameState.isPaused = false;
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    //pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
        gameState.isPaused = true;
    }

    //resumes game
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        gameState.isPaused = false;
    }

    //resets the game. thart at level 0.
    //should debatably use game manager to load scene 0. not sure
    public void ResetGame()
    {
        gameState.timer = 0f;
        gameState.currentLevel = 0;
        SceneManager.LoadScene("level 0");
        ResumeGame();
    }

    //exits game
    public void ExitGame()
    {
        Application.Quit();
    }

    //toggles timer visibility
    public void ToggleTimer()
    {
        //using the IUiManager interface instead of the UiManagerScript class for flexibility
        UiManager.GetComponent<IUiManager>().ToggleTimer();
    }
}