
using System.Collections;
using UnityEngine;

public class StarmanPowerup : BasePowerup
{
    // setup this object's type
    private Vector3 originalpos;
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        GetComponent<BoxCollider2D>().enabled = false;
        originalpos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player

            // then destroy powerup (optional)
            consumed = true;
            GetComponent<BoxCollider2D>().enabled = false;
            rigidBody.linearVelocity = UnityEngine.Vector2.zero;
            rigidBody.bodyType = RigidbodyType2D.Static;
            transform.position = originalpos;
            GetComponentInChildren<Animator>().SetBool("consumed", consumed);
            PowerUpCollected.Invoke(this);

        }
        else if (col.gameObject.layer == 8) // else if hitting Pipe, flip travel direction
        {
            var collisionPoint = col.collider.ClosestPoint(transform.position);
            var collisionNormal = new UnityEngine.Vector2(transform.position.x, transform.position.y) - collisionPoint;
            if (spawned && collisionNormal.y < 0.01f)
            {
                goRight = !goRight;
                rigidBody.AddForce((goRight ? 1 : -1) * 6 * UnityEngine.Vector2.right, ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        GetComponentInChildren<AudioSource>().Play();
        StartCoroutine(Enable_and_move_mushroom());
    }

    IEnumerator Enable_and_move_mushroom()
    {
        yield return new WaitForSecondsRealtime(1);
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

    public override void GameRestart()
    {
        consumed = false;
        spawned = false;
        GetComponentInChildren<Animator>().SetBool("consumed", consumed);
        GetComponentInChildren<Animator>().SetBool("spawned", spawned);
        GetComponentInChildren<Animator>().SetTrigger("gameRestart");
        GetComponent<BoxCollider2D>().enabled = false;
        rigidBody.linearVelocity = UnityEngine.Vector2.zero;
        rigidBody.bodyType = RigidbodyType2D.Static;
        transform.position = originalpos;

    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        BuffStateController mario;
        bool result = i.TryGetComponent(out mario);
        if (result)
        {
            mario.SetPowerup(this.powerupType);
        }

    }
}