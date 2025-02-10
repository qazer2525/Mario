
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPowerup : BasePowerup
{

    public UnityEvent<IPowerup> PowerUpCollected;
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
        PowerUpCollected.Invoke(this);
    }

    public override void GameRestart()
    {
        spawned = false;
    }
    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        bool result = i.TryGetComponent(out GameManager manager);
        if (result)
        {
            manager.IncreaseScore(1);
        }
    }
}