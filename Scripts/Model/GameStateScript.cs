using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

[CreateAssetMenu(fileName = "New GameState", menuName = "GameState")]
public class GameStateScript : ScriptableObject
{
    private int CurrentLevel = 0;
    public int currentLevel
    {
        get { return CurrentLevel; }
        set { CurrentLevel = value; }
    }

    private bool IsPaused = false;
    public bool isPaused
    {
        get { return IsPaused; }
        set { IsPaused = value; }
    }

    private bool TimerVisible = false;
    public bool timerVisible
    {
        get { return TimerVisible; }
        set { TimerVisible = value; }
    }

    private float DeathCount = 0f;
    public float deathCount
    {
        get { return DeathCount; }
        set { DeathCount = value; }
    }

    private float Timer = 0f;
    public float timer
    {
        get { return Timer; }
        set { Timer = value; }
    }

    private string UserId = "";
    public string userId
    {
        get { return UserId; }
        set { UserId = value; }
    }

    private bool[] AnsweredQuestions = new bool[5];
    public bool[] answeredQuestions
    {
        get { return AnsweredQuestions; }
        set { AnsweredQuestions = value; }
    }

    private int[] Attempts = { 0, 0, 0, 0, 0 };
    public int[] attempts
    {
        get { return Attempts; }
        set { Attempts = value; }
    }

    public void ResetState()
    {
        currentLevel = 0;
        timer = 0f;
        isPaused = false;
        userId = "";
        answeredQuestions = new bool[5];
        FirebaseAuth.DefaultInstance.SignOut();
    }
}
