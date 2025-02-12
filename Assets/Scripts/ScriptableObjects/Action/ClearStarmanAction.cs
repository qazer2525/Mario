using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearStarman")]
public class ClearStarmanAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.currentPowerupType = PowerupType.Default;
    }
}