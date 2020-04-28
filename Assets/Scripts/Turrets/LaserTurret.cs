public class LaserTurret : AttackingTurret
{
    public const int Cost = 30;

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Laser;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
