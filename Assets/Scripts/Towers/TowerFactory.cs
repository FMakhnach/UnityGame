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

    private Vector3 spawnPosition = new Vector3(0f, 100f, 0f);

    public Tower CreateLaserTower()
    {
        Tower tower = Instantiate(laserTowerPrefab, spawnPosition, Quaternion.identity);
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
        Tower tower = Instantiate(mgTowerPrefab, spawnPosition, Quaternion.identity);
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
