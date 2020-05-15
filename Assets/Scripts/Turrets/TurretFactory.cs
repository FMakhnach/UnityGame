using UnityEngine;

[CreateAssetMenu(menuName = "Turret Factory")]
public class TurretFactory : ScriptableObject
{
    [SerializeField]
    private Material laserTurretMaterial;
    [SerializeField]
    private Material mgTurretMaterial;

    public LaserTurret CreateLaserTurret(PlayerManager owner)
    {
        LaserTurret turret = PoolManager.Instance.GetLaser();
        turret.Owner = owner;
        foreach (var renderer in turret.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.CompareTag("MaterialChange"))
            {
                renderer.material = laserTurretMaterial;
            }
        }
        return turret;
    }
    public MachineGunTurret CreateMGTurret(PlayerManager owner)
    {
        MachineGunTurret turret = PoolManager.Instance.GetMG();
        turret.Owner = owner;
        foreach (var renderer in turret.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.CompareTag("MaterialChange"))
            {
                renderer.material = mgTurretMaterial;
            }
        }
        return turret;
    }
}