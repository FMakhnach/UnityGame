using UnityEngine;

[RequireComponent(typeof(OnMouseOverButtonPanel))]
public class MachineGunTurret : AttackingTurret
{
    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.MG;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
