using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Question = 0,
    Normal = 1
}
public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    public BasePowerup powerup; // reference to this question box's powerup
    public BlockType blocktype;

    public void Awake()
    {
        // subscribe to Game Restart event
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        {
            // show disabled sprite
            GetComponent<Animator>().SetTrigger("spawned");
            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");
            if (blocktype == BlockType.Question)
            {
                StartCoroutine(StopBounce());
            }
        }
    }

    IEnumerator StopBounce()
    {
        yield return new WaitForSecondsRealtime(2f);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void GameRestart()
    {
        GetComponent<Animator>().SetTrigger("OnGameRestart");
    }

}