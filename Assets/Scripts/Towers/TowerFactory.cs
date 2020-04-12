using UnityEngine;

[CreateAssetMenu()]
public class TowerFactory : ScriptableObject
{
    [SerializeField]
    private Tower laserTowerPrefab;
    [SerializeField]
    private Tower mgTowerPrefab;

    [SerializeField]
    private Material laserTowerMaterial;
    [SerializeField]
    private Material mgTowerMaterial;

    public Tower CreateLaserTower()
    {
        Tower tower = Instantiate(laserTowerPrefab);
        tower.gameObject.GetComponentInChildren<Renderer>().material = laserTowerMaterial;
        return tower;
    }
    public Tower CreateMGTower()
    {
        Tower tower = Instantiate(mgTowerPrefab);
        tower.gameObject.GetComponentInChildren<Renderer>().material = mgTowerMaterial;
        return tower;
    }
}
