using UnityEngine;

[CreateAssetMenu()]
public class TowerFactory : ScriptableObject
{
    [SerializeField]
    private Tower towerPrefab;

    public Tower GetTower(TowerTile tile)
    {
        Tower tower = Instantiate(towerPrefab);
        tower.PlaceOn(tile);
        return tower;
    }
}
