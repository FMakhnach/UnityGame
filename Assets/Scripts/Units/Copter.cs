public class Copter : Unit
{
    protected override void Aim()
    {
        body.transform.LookAt(currentTarget.TargetPoint.position);
    }
    protected override Projectile GetProjectile()
        => PoolManager.Instance.GetCopterProjectile();

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Copter;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
