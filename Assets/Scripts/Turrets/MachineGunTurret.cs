using UnityEngine;

[RequireComponent(typeof(OnMouseOverButtonPanel))]
public class MachineGunTurret : AttackingTurret
{
    protected override Projectile GetProjectile()
        => PoolManager.Instance.GetMGProjectile();
    protected override ParticleSystem GetShootEffect()
        => PoolManager.Instance.GetMGShootEffect();

    private void Start()
    {
        Panel = ObjectInfoPanelController.Instance.MG;
        GetComponent<OnMouseOverInfoPanel>().panel = Panel;
        damageableBehaviour.healthText = Panel.healthLabel;
    }
}
