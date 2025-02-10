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
        gameScore.SetValue(0);
        scoreText = gameUI.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: " + gameScore.Value.ToString());
    }

    public void SetScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + gameScore.Value.ToString();
    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameUI.SetActive(false);
        scoreText = gameOverPanel.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: " + gameScore.Value.ToString());
        gameOverPanel.transform.Find("HighScore").GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
    }
}
