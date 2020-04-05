using UnityEngine;

/// <summary>
/// Just for easy generating grid while developing.
/// </summary>
public class TilesGenerator : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Spawn spawnPrefab;
    [SerializeField]
    private RoadConfiguration road;

    private void Awake()
    {
        GameObject tileParent = new GameObject("Tiles");

        Tile[,] tiles = new Tile[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Tile tile = Instantiate(tilePrefab, tileParent.transform);
                tile.PlaceOnMap(i, j);
                tile.name = tile.ToString();
                tiles[i, j] = tile;
            }
        }

        // Creating a path of tiles.
        var path = new Tile[road.coordinates.Length];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = tiles[road.coordinates[i].x, road.coordinates[i].y].GetComponentInParent<Tile>();
        }
        // Creating a spawn object with path.
        Spawn spawn = Instantiate(spawnPrefab, path[0].GetComponentInChildren<MeshCollider>().transform);
        spawn.Path = path;
        path[0].isOccupied = true;
    }
}
