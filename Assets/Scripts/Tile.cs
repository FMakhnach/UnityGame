using UnityEngine;

/// <summary>
/// Represents a hexagonal tile.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Tile : MonoBehaviour
{
    private int x;
    private int y;
    private TileContent content;

    [HideInInspector]
    public bool isOccupied;

    // Magic coordinate system.
    public int X => x;
    public int Y => y;

    public void PlaceOnMap(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(1.5f * x, 0, (1.73205f * y) + 0.86602f * (x % 2));
    }

    public bool TryToPlace(TileContent prefab)
    {
        if (isOccupied)
        {
            return false;
        }
        else
        {
            content = Instantiate(prefab);
            Vector3 pos = this.transform.position;
            // Need 0.2 to place obj above the tile.
            pos.y += 0.2f ;
            content.transform.position = pos;
            isOccupied = true;
            return true;
        }
    }

    // FOR DEBUGGING
    public override string ToString()
        => "(" + x + ", " + y + ")";

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit is PlayerUnit)
        {
            unit.UpdateDestination(this);
        }
        else if (unit is EnemyUnit)
        {
            unit.UpdateDestination(this);
        }
    }
}
