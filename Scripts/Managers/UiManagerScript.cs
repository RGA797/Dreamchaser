using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class UiManagerScript : MonoBehaviour, IUiManager
{
    
    private bool timerEnabled = true;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timerText;

    public GameStateScript gameState;

    // Start is called before the first frame update
    void Start()
    {
        timerText.gameObject.SetActive(gameState.timerVisible);
    }

    // Update is called once per frame
    private void Update()
    {
        SetLevel(gameState.currentLevel);
        if (timerEnabled)
        {
            gameState.timer += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameState.timer);
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}",
                                            timeSpan.Minutes,
                                            timeSpan.Seconds,
                                            timeSpan.Milliseconds);
        }

        

    }

    public void ToggleTimer()
    {
        gameState.timerVisible = !gameState.timerVisible;
        timerText.gameObject.SetActive(gameState.timerVisible);
    }

    public void SetLevel(int level)
    {
        gameState.currentLevel = level;


        if(level > 5)
        {
            levelText.text = "Victory!";
        }
        else
        {
            levelText.text = "Level: " + gameState.currentLevel.ToString();
        }
       
    }

    public void IncrementLevel()
    {
        gameState.currentLevel++;
        levelText.text = "Level: " + gameState.currentLevel.ToString();
    }

}
