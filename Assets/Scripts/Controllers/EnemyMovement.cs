using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    public Vector3 startPosition;

    private Animator animator;
    public AudioSource StompAudio;

    private bool alive = true;

    public UnityEvent DamagePlayer;

    public UnityEvent BouncePlayer;
    public UnityEvent<int> IncreaseScore;
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // get the starting position
        startPosition = transform.localPosition;
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (alive)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }

    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     {
    //         GetComponent<Collider2D>().isTrigger = false;
    //         var collisionPoint = col.collider.ClosestPoint(transform.position);
    //         var collisionNormal = collisionPoint - new Vector2(transform.position.x, transform.position.y);
    //         if (col.gameObject.CompareTag("Player") && alive && collisionNormal.y <= 0 && col.gameObject.GetComponent<MarioStateController>().currentState.name != "DeadMario")
    //         {
    //             if (col.gameObject.GetComponent<BuffStateController>().currentState.name == "Invincible")
    //             {
    //                 BurnDeath(collisionNormal.x);
    //             }
    //             else
    //             {
    //                 DamagePlayer.Invoke();
    //             }

    //         }
    //         else if (collisionNormal.y > 0 && col.gameObject.CompareTag("Player") && alive && col.gameObject.GetComponent<MarioStateController>().currentState.name != "DeadMario")
    //         {
    //             EnemyDeath();
    //         }
    //         else if (col.gameObject.CompareTag("Fireball") && alive)
    //         {
    //             BurnDeath(collisionNormal.x);
    //         }
    //     }
    // }
    void OnTriggerEnter2D(Collider2D other)
    {
        var collisionPoint = other.ClosestPoint(transform.position);
        var collisionNormal = collisionPoint - new Vector2(transform.position.x, transform.position.y);
        if (other.gameObject.CompareTag("Player") && alive && collisionNormal.y <= 0 && other.gameObject.GetComponent<MarioStateController>().currentState.name != "DeadMario")
        {
            if (other.gameObject.GetComponent<BuffStateController>().currentState.name == "Invincible")
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                BurnDeath(collisionNormal.x);
            }
            else
            {
                DamagePlayer.Invoke();
            }

        }
        else if (collisionNormal.y > 0 && other.gameObject.CompareTag("Player") && alive && other.gameObject.GetComponent<MarioStateController>().currentState.name != "DeadMario")
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            EnemyDeath();
        }
        else if (other.gameObject.CompareTag("Fireball") && alive)
        {
            BurnDeath(collisionNormal.x);
        }
    }

    public void EnemyDeath()
    {
        if (alive)
        {
            alive = false;
            StompAudio.Play();
            animator.SetBool("stomped", !alive);
            GetComponent<BoxCollider2D>().enabled = false;
            IncreaseScore.Invoke(1);
            BouncePlayer.Invoke();
        }
    }

    public void BurnDeath(float x)
    {
        if (alive)
        {
            alive = false;
            animator.SetBool("burned", !alive);
            Debug.Log(x);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * x * 50, ForceMode2D.Impulse);
            IncreaseScore.Invoke(1);
            StartCoroutine(StartDisable());
        }
    }

    IEnumerator StartDisable()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
        alive = true;
        animator.SetBool("burned", !alive);
        animator.SetBool("stomped", !alive);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().enabled = true;
        animator.SetTrigger("GameRestart");


    }

}