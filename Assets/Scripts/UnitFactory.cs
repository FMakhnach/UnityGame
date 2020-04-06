using UnityEngine;

[CreateAssetMenu()]
public class UnitFactory : ScriptableObject
{
    [SerializeField]
    private Unit unitPrefab;

    public Unit GetUnit(SpawnTile spawn)
    {
        Unit unit = Instantiate(unitPrefab);
        unit.PlaceOn(spawn);
        return unit;
    }
}
