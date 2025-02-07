
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
    }
    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        GetComponentInChildren<AudioSource>().Play();
        GameManager.instance.IncreaseScore(1);
    }

    public override void GameRestart()
    {
        spawned = false;
    }
    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
    }
}