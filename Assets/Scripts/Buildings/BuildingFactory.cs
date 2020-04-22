using UnityEngine;

[CreateAssetMenu]
public class BuildingFactory : ScriptableObject
{
    [SerializeField]
    private Plant plantPrefab;

    public Plant CreatePlant()
    {
        Plant plant = Instantiate(plantPrefab);
        return plant;
    }
}
