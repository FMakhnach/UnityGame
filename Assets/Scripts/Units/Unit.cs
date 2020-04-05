using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    /// <summary>
    /// Current destination. Null indicates absence of destination.
    /// </summary>
    private Vector3? destination;
    /// <summary>
    /// Units movement speed.
    /// </summary>
    private float speed = 2f;
    /// <summary>
    /// Path of the unit. 
    /// </summary>
    private Tile[] path;
    private int currentDestinationTileId = 0;

    public Tile[] Path
    {
        get => path;
        set
        {
            if (path != null)
            {
                Debug.LogError("Trying to rewrite path wtf");
            }
            else
            {
                path = value;
            }
        }
    }

    /// <summary>
    /// Is called when stepping into a new tile collider.
    /// </summary>
    public void UpdateDestination(Tile reachedTile)
    {
        // If we really stepped onto the wanted tile.
        if (reachedTile == path[currentDestinationTileId])
        {
            Tile nextTile = GetNextTileOnPath();
            if (nextTile != null)
            {
                Vector3 pos = nextTile.transform.position;
                pos.y = transform.position.y;
                destination = pos;
            }
        }
    }

    private void Update()
    {
        if (destination != null)
        {
            MoveToDestination();
        }
    }
    /// <summary>
    /// Moves the unit towards the destination point.
    /// </summary>
    private void MoveToDestination()
    {
        Vector3 deltaPath = (Vector3)destination - transform.position;
        deltaPath.y = 0;
        deltaPath = deltaPath.normalized * speed * Time.deltaTime;
        transform.Translate(deltaPath);
        // This looks pretty shitty, would be great to improve.
        // Wo this unit starts to vibrate in the end.
        if ((transform.position - (Vector3)destination).sqrMagnitude < 0.0001f)
        {
            destination = null;
            Debug.Log("Quest Completed!");
        }
    }
    private Tile GetNextTileOnPath()
        => currentDestinationTileId == (path.Length - 1) ? null : path[++currentDestinationTileId];
}
