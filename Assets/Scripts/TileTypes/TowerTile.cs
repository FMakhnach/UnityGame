public class TowerTile : Tile
{
    public bool IsOccupied { get; set; }

    private void Awake()
    {
        IsOccupied = false;
    }
}
