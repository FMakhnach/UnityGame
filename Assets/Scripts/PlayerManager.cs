using UnityEngine;

/// <summary>
/// Manages players units. 
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private UnitFactory unitFactory;
    [SerializeField]
    private TowerFactory towerFactory;
    [SerializeField]
    private Alignment alignment;

    /// <summary>
    /// Spawns a unit on a given spawnpoint and gives him a path.
    /// </summary>
    public void SpawnUnit(SpawnTile spawn)
    {
        unitFactory.CreateUnit().SpawnOn(spawn, Alignment.Computer);
    }
    public void SpawnLaserTower(TowerTile tile)
    {
        towerFactory.CreateLaserTower().SpawnOn(tile, alignment);
    }
    public void SpawnMGTower(TowerTile tile)
    {
        towerFactory.CreateMGTower().SpawnOn(tile, alignment);
    }
}
