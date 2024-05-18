using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countdownText;

   

    int score = 0;
    float timer = 0;
    float countdownTimer = 3.0f;
    float tempTimer = 0.0f;
    bool startCountdown = false;

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
        

        UpdateGameState(state);
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

    private void CountdownToBeginLevel()
    {
        //countdownText.text = "Get Ready!";
        
        tempTimer += 1.0f * Time.deltaTime;

        if (tempTimer < 3.0f)
        {
            countdownText.text = "Get Ready!";
        }

        if (tempTimer > 3.0f)
        {
            startCountdown = true;
        }

        if (startCountdown)
        {
            countdownTimer -= Time.deltaTime;

            


            if (countdownTimer > 2.01f && countdownTimer <= 3.0f)
            {
                countdownText.text = "3!";
            }

            if (countdownTimer > 1.01f && countdownTimer <= 2.0f)
            {
                countdownText.text = "2!";
            }

            if (countdownTimer > 0.01f && countdownTimer <= 1.0f)
            {
                countdownText.text = "1!";
            }

            if (countdownTimer > -2.00f && countdownTimer <= 0.01f)
            {
                countdownText.text = "Clean!";
            }

            if (countdownTimer < -2.00f)
            {
                UpdateGameState(GameState.LevelInProgress);
                countdownText.enabled = false;
            }
        }
    }

    private void HandleReadyLevel()
    {
       CountdownToBeginLevel();
    }

    private void HandleLevelInProgress() 
    {
        UpdateTimer();
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