public class Copter : Unit
{
    public const int Cost = 30;

    protected override void Aim()
    {
        body.transform.LookAt(currentTarget.TargetPoint.position);
    }

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Copter;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
