/// <summary>
/// Represents object that can be placed on a certain tile type.
/// </summary>
public interface ISpawnable<T> where T : Tile
{
    void SpawnOn(T tile, Alignment alignment);
}
