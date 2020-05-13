using UnityEngine;

[CreateAssetMenu(menuName = "Building Factory")]
public class BuildingFactory : ScriptableObject
{
    [SerializeField]
    private Plant plantPrefab;
    [SerializeField]
    private Material plantMaterial;

    public Plant CreatePlant(PlayerManager owner)
    {
        Plant plant = PoolManager.Instance.GetPlant();
        plant.Owner = owner;
        foreach (var renderer in plant.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer.gameObject.CompareTag("MaterialChange"))
            {
                renderer.material = plantMaterial;
            }
        }
        return plant;
    }
}
