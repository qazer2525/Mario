using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    // Start is called before the first frame update
    private TextMeshProUGUI scoreText;
    public GameObject enemies;

    public JumpOverGoomba jumpOverGoomba;

    public GameObject gameOver;

    public GameObject GameUI;

    // for animation
    public Animator marioAnimator;

    public AudioClip marioDeath;
    public float deathImpulse = 15;

    public AudioSource marioAudio;

    public Transform gameCamera;
    // state
    [System.NonSerialized]
    public bool alive = true;

    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8);
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        gameOver.SetActive(false);
        GameUI.SetActive(true);
        scoreText = GameUI.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocity.x > 0.1f && onGroundState == true)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocity.x < -0.1f && onGroundState == true)
                marioAnimator.SetTrigger("onSkid");
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    // FixedUpdate is called 50 times a second
    void FixedUpdate()

    {
        if (alive)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                // check if it doesn't go beyond maxSpeed
                if (onGroundState == false && marioBody.linearVelocityX > 0 && Input.GetKey(KeyCode.A))
                {
                    marioBody.AddForce(movement * speed * 4);
                }
                else if (onGroundState == false && marioBody.linearVelocityX < 0 && Input.GetKey(KeyCode.D))
                {
                    marioBody.AddForce(movement * speed * 4);
                }
                else if (marioBody.linearVelocity.magnitude < maxSpeed + marioBody.linearVelocityY)
                    marioBody.AddForce(movement * speed);
            }

            // // stop
            // if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            // {
            //     // stop
            //     marioBody.linearVelocity = Vector2.zero;
            // }


            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
            }
            // update animator state    
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Collided with goomba!" + jumpOverGoomba.score.ToString());
            // play death animation
            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    public void RestartButtonCallback(int input)
    {
        //Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    public void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-8f, -2.27f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        // reset score
        jumpOverGoomba.score = 0;
        gameOver.SetActive(false);
        GameUI.SetActive(true);
        scoreText = GameUI.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: " + jumpOverGoomba.score.ToString());
        marioBody.linearVelocity = Vector2.zero;
        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
        gameCamera.position = new Vector3(-0.6f, 0.1f, -10);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        //update score, change UIs
        gameOver.SetActive(true);
        GameUI.SetActive(false);
        scoreText = gameOver.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText("Score: " + jumpOverGoomba.score.ToString());
        // set gameover scene
        //gameManager.GameOver(); // replace this with whichever way you triggered the game over screen for Checkoff 1

    }
}
