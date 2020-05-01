public class MachineGunTurret : AttackingTurret
{
    public const int Cost = 50;

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.MG;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
