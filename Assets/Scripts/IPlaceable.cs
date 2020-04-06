/// <summary>
/// Represents object that can be placed on a certain tile type.
/// </summary>
public interface IPlaceable<T> where T : Tile
{
    void PlaceOn(T tile);
}
