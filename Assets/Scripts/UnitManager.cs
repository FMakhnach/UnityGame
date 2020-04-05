using UnityEngine;

/// <summary>
/// Manages players units. 
/// </summary>
public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private Unit unitPrefab;
    // private UnitFacory factory;

    /// <summary>
    /// TO REFACTOR {incapsulate instantiating to a factory}
    /// Spawns a unit on a given spawnpoint and gives him a path.
    /// </summary>
    public void SpawnUnit(Spawn spawn)
    {
        Vector3 pos = spawn.transform.position;
        // Making unit stay on the ground [0.7 FOR NOW]
        pos.y += 0.7f;
        Unit unit = Instantiate(unitPrefab, pos, Quaternion.identity);
        unit.Path = spawn.Path;
    }
}
