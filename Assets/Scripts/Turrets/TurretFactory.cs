using UnityEngine;

[CreateAssetMenu(menuName = "Turret Factory")]
public class TurretFactory : ScriptableObject
{
    [SerializeField]
    private LaserTurret laserTurretPrefab;
    [SerializeField]
    private MachineGunTurret mgTurretPrefab;

    [SerializeField]
    private Material laserTurretMaterial;
    [SerializeField]
    private Material mgTurretMaterial;

    private Vector3 spawnPosition = new Vector3(0f, 100f, 0f);

    public LaserTurret CreateLaserTurret(PlayerManager owner)
    {
        LaserTurret turret = Instantiate(laserTurretPrefab, spawnPosition, Quaternion.identity);
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
        MachineGunTurret turret = Instantiate(mgTurretPrefab, spawnPosition, Quaternion.identity);
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