using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject gameUI;

    public IntVariable gameScore;
    public void Awake()
    {
        // subscribe to Game Restart event
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameOverPanel.SetActive(false);
        gameUI.SetActive(true);
        scoreText = gameUI.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: " + 0.ToString());
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver(int score)
    {
        gameOverPanel.SetActive(true);
        gameUI.SetActive(false);
        scoreText = gameOverPanel.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: " + score.ToString());
        gameOverPanel.transform.Find("HighScore").GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
    }
}
