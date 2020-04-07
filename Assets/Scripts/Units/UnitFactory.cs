using UnityEngine;

[CreateAssetMenu()]
public class UnitFactory : ScriptableObject
{
    [SerializeField]
    private Unit unitPrefab;

    public Unit CreateUnit()
    {
        Unit unit = Instantiate(unitPrefab);
        return unit;
    }
}
