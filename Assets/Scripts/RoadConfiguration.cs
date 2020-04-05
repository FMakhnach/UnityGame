using UnityEngine;

[CreateAssetMenu(fileName = "RoadConfiguration", menuName = "MyAssets/RoadConfiguration", order = 1)]
public class RoadConfiguration : ScriptableObject
{
    public Vector2Int[] coordinates;
}
