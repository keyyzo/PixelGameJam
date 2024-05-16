using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    int score = 0;
    float timer = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        { 
            instance = this;
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.ReadyLevel);

        scoreText.text = "Score: " + score.ToString();
        
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void UpdateGameState(GameState newState)
    { 
        state = newState;

        switch (newState)
        {
            case GameState.ReadyLevel:
                HandleReadyLevel();
                break;
            case GameState.LevelInProgress:
                HandleLevelInProgress();
                break;
            case GameState.LevelFinished:
                HandleLevelFinished();
                break;
            case GameState.Rating:
                HandleRating();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState, null);
        }


        OnGameStateChanged?.Invoke(newState);
    }


    private void HandleReadyLevel()
    { 
        
    }

    private void HandleLevelInProgress() 
    {
    
    }

    private void HandleRating() 
    {
    
    }

    private void HandleLevelFinished() 
    {
    
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateTimer()
    {
        timer += 1.0f * Time.deltaTime;
        timerText.text = "Timer: " + timer.ToString("F2");
    }

}

public enum GameState
{
    ReadyLevel,
    LevelInProgress,
    LevelFinished,
    Rating
}