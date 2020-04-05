using UnityEngine;

/// <summary>
/// For tests mostly.
/// </summary>
public class PrimitiveInput : MonoBehaviour
{
    public Tower towerPrefab;
    public UnitManager unitManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceUnit();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            PlaceTower();
        }
    }

    /// <summary>
    /// Delete someday.
    /// </summary>
    private void PlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Tile tile;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if ((tile = hit.collider.gameObject.GetComponentInParent<Tile>()) != null)
            {
                // FOR DEBUGGING
                Debug.Log(tile);
                tile.TryToPlace(towerPrefab);
            }
        }
    }
    private void PlaceUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Spawn spawn;
            if ((spawn = hit.collider.gameObject.GetComponentInChildren<Spawn>()) != null)
            {
                unitManager.SpawnUnit(spawn);
            }
        }
    }
}
