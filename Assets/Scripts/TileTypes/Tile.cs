using UnityEngine;

/// <summary>
/// Represents a hexagonal tile.
/// </summary>
public class Tile : MonoBehaviour
{
    private int x;
    private int y;

    // Magic coordinate system.
    public int X => x;
    public int Y => y;

    public void PlaceOnMap(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(1.5f * x, 0, (1.73205f * y) + 0.86602f * (x % 2));
    }

    // FOR DEBUGGING
    public override string ToString()
        => "(" + x + ", " + y + ")";
}
