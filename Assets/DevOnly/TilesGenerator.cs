using UnityEngine;

/// <summary>
/// Just for easy generating grid while developing.
/// </summary>
public class TilesGenerator : MonoBehaviour
{
    [SerializeField]
    private Tile emptyTilePrefab;
    [SerializeField]
    private SpawnTile spawnTilePrefab;
    [SerializeField]
    private RoadTile roadTilePrefab;
    [SerializeField]
    private TowerTile towerTilePrefab;
    [SerializeField]
    private LevelConfiguration lvlConfig;
    [SerializeField]
    private GameObject tileParent;

    private void Awake()
    {
        //GameObject tileParent = new GameObject("Tiles");

        Tile[,] tiles = new Tile[10, 10];

        // Roads (1 for now)
        var road = lvlConfig.roads[0];
        var spawn = Instantiate(spawnTilePrefab, tileParent.transform);
        Tile[] path = new Tile[road.Length - 1];
        for (int i = 1; i < road.Length; i++)
        {
            RoadTile tile = Instantiate(roadTilePrefab, tileParent.transform);
            tile.PlaceOnMap(road[i].x, road[i].y);
            tile.name = tile.ToString();
            tiles[road[i].x, road[i].y] = tile;
            path[i - 1] = tile;
        }
        spawn.Path = path;

        spawn.PlaceOnMap(road[0].x, road[0].y);
        spawn.name = spawn.ToString();
        tiles[road[0].x, road[0].y] = spawn;

        // Tower tiles
        foreach (var t in lvlConfig.towerTilesCoordinates)
        {
            TowerTile tile = Instantiate(towerTilePrefab, tileParent.transform);
            tile.PlaceOnMap(t.x, t.y);
            tile.name = tile.ToString();
            tiles[t.x, t.y] = tile;
        }

        // Everything else is empty
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (tiles[i, j] == null)
                {
                    Tile tile = Instantiate(emptyTilePrefab, tileParent.transform);
                    tile.PlaceOnMap(i, j);
                    tile.name = tile.ToString();
                    tiles[i, j] = tile;
                }
            }
        }
    }
}
