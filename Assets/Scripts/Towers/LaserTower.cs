public class LaserTower : AttackingTower
{
    public const int Cost = 30;

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Laser;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthText;
    }
}
