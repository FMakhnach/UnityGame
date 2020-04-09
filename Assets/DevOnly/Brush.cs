using System;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private Transform parent;

    private Action currentBrush;

    public void ClearBrush(bool toggle)
    {
        if (toggle)
        {
            currentBrush = null;
        }
    }
    public void OnDeleteToggle(bool toggle)
    {
        if (toggle)
        {
            currentBrush = DeleteTile;
        }
    }
    private void DeleteTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Tile tile = hit.collider.GetComponentInParent<Tile>();
                if (tile != null)
                {
                    Destroy(tile.gameObject);
                }
            }
        }
    }

    private void PlaceTile(Tile tile)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(tile, hit.collider.transform.parent.transform.position, Quaternion.identity, parent);
            }
        }
    }
    public void OnGrassEmptyToggle(bool toggle)
    {
        if (toggle)
        {
            currentBrush = PlaceGrassEmpty;
        }
    }
    private void PlaceGrassEmpty() => PlaceTile(tilePrefab);

    private void Update()
    {
        currentBrush?.Invoke();
    }
}
