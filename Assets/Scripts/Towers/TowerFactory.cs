using UnityEngine;

[CreateAssetMenu()]
public class TowerFactory : ScriptableObject
{
    [SerializeField]
    private Tower laserTowerPrefab;
    [SerializeField]
    private Tower mgTowerPrefab;

    public Tower CreateLaserTower()
    {
        Tower tower = Instantiate(laserTowerPrefab);
        return tower;
    }
    public Tower CreateMGTower()
    {
        Tower tower = Instantiate(mgTowerPrefab);
        return tower;
    }
}
