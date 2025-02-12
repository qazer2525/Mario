using UnityEngine;
using UnityEngine.Events;


class PowerUpContoller : MonoBehaviour
{
    public UnityEvent<IPowerup> PowerUpAffectsManager;
    public UnityEvent<IPowerup> PowerUpAffectsPlayer;

    public void FilterAndCastPowerUp(IPowerup powerup)

    {
        if (powerup.powerupType == PowerupType.Coin)
        {
            PowerUpAffectsManager.Invoke(powerup);
        }
        else
        {
            PowerUpAffectsPlayer.Invoke(powerup);
        }
    }
}