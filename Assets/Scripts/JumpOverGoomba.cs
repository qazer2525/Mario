using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverGoomba : MonoBehaviour
{
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        // mario jumps
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        // when jumping, and Goomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.35f)
            {
                gameManager.IncreaseScore(1);
                countScoreState = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }


    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            //Debug.Log("on ground");
            return true;
        }
        else
        {
            //Debug.Log("not on ground");
            return false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}
