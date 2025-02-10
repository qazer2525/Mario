using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager
{
    // events
    public UnityEvent gameStart;
    public UnityEvent UpdateScore;
    public IntVariable gameScore;

    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        // gameStart.Invoke();
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        gameScore.SetValue(0);
        Time.timeScale = 1.0f;
        gameStart.Invoke();
        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        UpdateScore.Invoke();
    }

    public void RequestPowerUpEffect()
    {

    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
    }

    public void OnGamePaused()
    {
        Time.timeScale = 0.0f;
    }

    public void OnGameResumed()
    {
        Time.timeScale = 1.0f;
    }
    public void SceneSetup(Scene current, Scene next)
    {
        if (next.name.Contains("World"))
        {
            if (next.name == "World 1-1") gameScore.SetValue(0);
        }
    }

    public void IncreaseMarioLifeCount(int input)
    {

    }
}