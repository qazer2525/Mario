
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    // setup this object's type
    private UnityEngine.Vector3 originalpos;
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
        GetComponent<BoxCollider2D>().enabled = false;
        originalpos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.layer);
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
        rigidBody.AddForce(UnityEngine.Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

    public override void GameRestart()
    {
        if (spawned && !consumed)
        {
            GetComponentInChildren<Animator>().SetBool("consumed", true);

        }
        consumed = false;
        GetComponentInChildren<Animator>().SetBool("consumed", consumed);
        spawned = false;
        GetComponent<BoxCollider2D>().enabled = false;
        rigidBody.linearVelocity = UnityEngine.Vector2.zero;
        rigidBody.bodyType = RigidbodyType2D.Static;
        transform.position = originalpos;

    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        bool result = i.TryGetComponent(out GameManager manager);
        if (result)
        {
            manager.IncreaseMarioLifeCount(1);
        }

    }
}