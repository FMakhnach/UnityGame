using UnityEngine;

/// <summary>
/// === NOT USED FOR NOW ===
/// Stores tiles of the map. Don't know if it'll be usefull.
/// </summary>
public class MapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tilesStorage;
    private Tile[,] mapTiles;

    public Tile[,] MapTiles => mapTiles;

    private void Awake()
    {
        GetTilesFromScene();
    }

    private void GetTilesFromScene()
    {
        Tile[] tiles = tilesStorage.GetComponentsInChildren<Tile>();
        mapTiles = new Tile[10, 10];
        foreach (var t in tiles)
        {
            mapTiles[t.X, t.Y] = t;
        }
    }
}
