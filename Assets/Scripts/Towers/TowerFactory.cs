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
        foreach (var renderer in tower.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.CompareTag("MaterialChange"))
            {
                renderer.material = laserTowerMaterial;
            }

        }
        return tower;
    }
    public Tower CreateMGTower()
    {
        Tower tower = Instantiate(mgTowerPrefab);
        foreach (var renderer in tower.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.CompareTag("MaterialChange"))
            {
                renderer.material = mgTowerMaterial;
            }
        }
        return tower;
    }
}
