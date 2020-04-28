using UnityEngine;

[CreateAssetMenu(menuName = "Building Factory")]
public class BuildingFactory : ScriptableObject
{
    [SerializeField]
    private Plant plantPrefab;

    public Plant CreatePlant(PlayerManager owner)
    {
        Plant plant = Instantiate(plantPrefab);
        plant.Owner = owner;
        return plant;
    }
}
