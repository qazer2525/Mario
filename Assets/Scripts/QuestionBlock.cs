using Unity.VisualScripting;
using UnityEngine;

public class QuestionBlock : MonoBehaviour


{
    private bool collected = false;
    public Animator questionblockanimator;

    public GameObject coin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // float mario_max_y = col.collider.bounds.center.y - col.collider.bounds.extents.y;
        // float block_min_y = col.otherCollider.bounds.center.y - col.otherCollider.bounds.extents.y;

        // float mario_min_x = col.collider.bounds.center.x - col.collider.bounds.extents.x;
        // float mario_max_x = col.collider.bounds.center.x + col.collider.bounds.extents.x;
        // float block_min_x = col.otherCollider.bounds.center.x - col.otherCollider.bounds.extents.x;
        // float block_max_x = col.otherCollider.bounds.center.x + col.otherCollider.bounds.extents.x;
        if (collected == false)
        {
            collected = true;
            questionblockanimator.SetBool("collected", collected);
            coin.GetComponent<Animator>().SetTrigger("OnCollision");
            coin.GetComponent<AudioSource>().Play();

        }
    }
    void OnCollected(int input)
    {
        transform.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public void GameRestart()
    {
        transform.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        collected = false;
        questionblockanimator.SetTrigger("OnGameRestart");
        questionblockanimator.SetBool("collected", collected);

    }
}
