
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;

    public AudioSource bgm;
    // Start is called before the first frame update

    public UnityEvent<GameObject> OnGamePaused;

    public UnityEvent<GameObject> OnGameResumed;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            if (bgm != null)
            {
                bgm.Pause();
            }
            image.sprite = playIcon;

        }
        else
        {
            if (bgm != null)
            {
                bgm.Play();
            }
            image.sprite = pauseIcon;
        }
    }
}
