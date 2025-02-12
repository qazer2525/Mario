
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FireFlowerPowerup : BasePowerup
{
    // setup this object's type
    private UnityEngine.Vector3 originalpos;
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.FireFlower;
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
            transform.position = originalpos;
            GetComponentInChildren<Animator>().SetBool("consumed", consumed);
            PowerUpCollected.Invoke(this);

        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        GetComponentInChildren<AudioSource>().Play();
        GetComponent<BoxCollider2D>().enabled = true;
    }


    public override void GameRestart()
    {
        consumed = false;
        GetComponentInChildren<Animator>().SetBool("consumed", consumed);
        spawned = false;
        GetComponentInChildren<Animator>().SetTrigger("gameRestart");
        GetComponent<BoxCollider2D>().enabled = false;
        transform.position = originalpos;

    }

    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        MarioStateController mario;
        bool result = i.TryGetComponent(out mario);
        if (result)
        {
            mario.SetPowerup(this.powerupType);
        }

    }
}