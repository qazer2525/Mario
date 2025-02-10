using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndWait : MonoBehaviour
{
    public CanvasGroup c;
    public PlayerState playerState;

    void Start()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {   
        playerState.BigMario = false;
        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            c.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        // once done, go to next scene
        SceneManager.LoadSceneAsync("World 1-1", LoadSceneMode.Single);
    }

    public void ReturnToMain()
    {
        // TODO
        Debug.Log("Return to main menu");
        SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
    }
}
