using System.Collections.Generic;
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

    // Not sure that I will need this.
    private List<Unit> playersUnits;
    private List<Tower> playerTowers;

    private void Awake()
    {
        playersUnits = new List<Unit>();
        playerTowers = new List<Tower>();
    }

    /// <summary>
    /// Spawns a unit on a given spawnpoint and gives him a path.
    /// </summary>
    public void SpawnUnit(SpawnTile spawn)
    {
        playersUnits.Add(unitFactory.GetUnit(spawn));
    }
    public void SpawnTower(TowerTile tile)
    {
        playerTowers.Add(towerFactory.GetTower(tile));
    }
}
