using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    private Animator animator;

    private GameManager gameManager;

    public AudioSource StompAudio;

    private bool alive = true;
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        // get the starting position
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
    void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void EnemyDeath()
    {
        alive = false;
        StompAudio.Play();
        animator.SetBool("alive", alive);
        GetComponent<BoxCollider2D>().enabled = false;
        gameManager.IncreaseScore(1);


    }
    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
        alive = true;
        animator.SetBool("alive", alive);
        GetComponent<BoxCollider2D>().enabled = true;
        animator.SetTrigger("GameRestart");


    }

}