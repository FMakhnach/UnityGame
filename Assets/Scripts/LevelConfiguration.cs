using UnityEngine;

[CreateAssetMenu()]
public class LevelConfiguration : ScriptableObject
{
    [System.Serializable]
    public struct Road
    {
        public Vector2Int[] roadCoordinates;
        public Vector2Int this[int i] => roadCoordinates[i];
        public int Length => roadCoordinates.Length;
    }
    public Road[] roads;
    public Vector2Int[] towerTilesCoordinates;
}