using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


//game manager class.
//responsibility: Changing scenes, and setting the appopriate values for wins/losses of stages.
public class GameManagerScript : MonoBehaviour, IGameManager
{
    public GameStateScript gameState;
    public UiManagerScript UiManager;
    public IMenuManager MenuManager;
    private bool quizEnabled = true;
    private int lastLevel = 5;

    //on init
    private void Start()
    {
        gameState.currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    //calls every grame
    private void Update()
    {

    }

    //returns unity scene index
    public int getCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    //changes scenes in accordance to game rules, and whether quiz is enabled
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != lastLevel)
        {
            if (!quizEnabled)
            {
                //gameState.currentLevel++;
                UiManager.IncrementLevel();
                LoadNextScene();
            }
            else if (gameState.answeredQuestions[SceneManager.GetActiveScene().buildIndex] == false){
                LoadQuestion();
            }
            else
            {
                UiManager.IncrementLevel();
                LoadNextScene();
            }
            
        }
        else
        {
            //loading victory
            SceneManager.LoadScene(11);
        }
     
    }

    //goes back to past level. increments deathcount.
    public void PastLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        if (level == 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (level == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            gameState.currentLevel -= SceneManager.GetActiveScene().buildIndex;
        }

        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            gameState.currentLevel -= SceneManager.GetActiveScene().buildIndex;
        }

        gameState.deathCount++;
    }

    //loads next scene in index
    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex));
    }

    //loads the question scene
    private void LoadQuestion()
    {
        SceneManager.LoadScene(13);
    }


    //quits the application
    public void ExitGame()
    {
        Application.Quit();
    }

    //local values reset on application quit
    private void OnApplicationQuit()
    {
        gameState.ResetState();
    }


}