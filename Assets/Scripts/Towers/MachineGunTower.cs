public class MachineGunTower : AttackingTower
{
    public const int Cost = 20;

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.MG;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthText;
    }
}
