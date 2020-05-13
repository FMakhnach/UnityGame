using UnityEngine;

[RequireComponent(typeof(OnMouseOverButtonPanel))]
public class LaserTurret : AttackingTurret
{
    protected override Projectile GetProjectile()
        => PoolManager.Instance.GetLaserProjectile();
    protected override ParticleSystem GetShootEffect()
        => PoolManager.Instance.GetLaserShootEffect();

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.Laser;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
