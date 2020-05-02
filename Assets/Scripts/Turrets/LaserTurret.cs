using UnityEngine;

[RequireComponent(typeof(OnMouseOverButtonPanel))]
public class LaserTurret : AttackingTurret
{
    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Laser;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
