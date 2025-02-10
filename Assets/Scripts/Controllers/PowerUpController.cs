using UnityEngine;
using UnityEngine.Events;


class PowerUpContoller : MonoBehaviour
{
    public UnityEvent<IPowerup> PowerUpAffectsManager;
    public UnityEvent<IPowerup> PowerUpAffectsPlayer;

    public void FilterAndCastPowerUp(IPowerup powerup)
    {
        powerup.ApplyPowerup(this);
    }
}