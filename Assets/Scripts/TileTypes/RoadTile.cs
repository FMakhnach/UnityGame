using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoadTile : Tile
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Unit>()?.UpdateDestination(this);
    }
}
