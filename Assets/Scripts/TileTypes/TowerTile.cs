public class TowerTile : Tile
{
    private bool isOccupied = false;
    /// <summary>
    /// [USELESS]
    /// </summary>
    private Tower tower;

    public bool IsOccupied => isOccupied;

    public void RecieveTower(Tower tower)
    {
        this.tower = tower;
        isOccupied = true;
    }
}
