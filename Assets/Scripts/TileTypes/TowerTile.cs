public class TowerTile : Tile
{
    private Tower tower;
    private bool isOccupied = false;
    public bool IsOccupied => isOccupied;

    public void RecieveTower(Tower tower)
    {
        this.tower = tower;
        isOccupied = true;
    }
}
