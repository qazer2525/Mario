using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class PlayerMovement : MonoBehaviour
{
    public GameConstants gameConstants;
    float deathImpulse;
    float upSpeed;
    float maxSpeed;
    float speed;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;

    public BoolVariable marioFaceRight;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    // for animation
    public Animator marioAnimator;

    public AudioSource marioDeath;
    public AudioSource marioAudio;

    private Transform SpawnPoint;

    // state
    [System.NonSerialized]
    public bool alive = true;

    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8);

    public MarioActions marioActions;

    private bool moving = false;

    private bool jumpedState = false;

    public UnityEvent<GameObject> killEnemy;

    public UnityEvent OnGameOver;
    public void Awake()
    {
    }
    void Start()
    {
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
        SpawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform;
        marioBody.transform.position = SpawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocityX));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            updateMarioShouldFaceRight(false);
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocityX > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            updateMarioShouldFaceRight(true);
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocityX < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) && !onGroundState)

        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    // FixedUpdate is called 50 times a second
    void FixedUpdate()

    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (!alive) return;
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

    public void MoveCheck(int value)
    {
        if (!alive) return;
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

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
        marioBody.transform.position = SpawnPoint.position;
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        marioBody.linearVelocity = Vector2.zero;
        // reset animation
        GetComponent<MarioStateController>().GameRestart();
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

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

    private void updateMarioShouldFaceRight(bool value)
    {
        faceRightState = value;
        marioFaceRight.SetValue(faceRightState);
    }
    public void DamageMario()
    {
        // GameOverAnimationStart(); // last time Mario dies right away

        // pass this to StateController to see if Mario should start game over
        // since both state StateController and MarioStateController are on the same gameobject, it's ok to cross-refer between scripts
        GetComponent<MarioStateController>().SetPowerup(PowerupType.Damage);
    }

    public void GameOver()
    {
        OnGameOver.Invoke();
    }
    public void RequestPowerUpEffect(IPowerup powerup)
    {
        powerup.ApplyPowerup(GetComponent<MarioStateController>());
    }
    // }
    // public void SetStartingPosition(Scene current, Scene next)
    // {
    //     if (next.name == "World 1-2")
    //     {
    //         // change the position accordingly in your World-1-2 case
    //         SpawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform;
    //         this.transform.position = SpawnPoint.position;
    //     }
    // }
}
