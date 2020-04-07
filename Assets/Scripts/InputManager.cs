using UnityEngine;
using System;

/// <summary>
/// For tests mostly.
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private Action mouseClickLeft;
    private Action mouseClickRight;
    private Action updateGhost;

    [SerializeField]
    private Ghost unitGhostPrefab;
    [SerializeField]
    private Ghost laserTowerGhostPrefab;
    [SerializeField]
    private Ghost mgTowerGhostPrefab;
    [SerializeField]
    private LayerMask ghostWorldPlacementMask;
    /// <summary>
    /// Current placeable object ghost in game.
    /// </summary>
    private Ghost currentGhost;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip wrongPlace;

    public void UnitButtonClicked()
    {
        ClearGhost();
        currentGhost = Instantiate(unitGhostPrefab, Input.mousePosition, Quaternion.identity);
        mouseClickLeft = PlaceUnit;
        mouseClickRight = Refresh;
        updateGhost = MoveGhostAfterCursor<SpawnTile>;
    }
    public void TowerButtonClicked()
    {
        ClearGhost();
        currentGhost = Instantiate(laserTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
        mouseClickLeft = PlaceLaserTower;
        mouseClickRight = Refresh;
        updateGhost = MoveGhostAfterCursor<TowerTile>;
    }
    public void MGTowerButtonClicked()
    {
        ClearGhost();
        currentGhost = Instantiate(mgTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
        mouseClickLeft = PlaceMGTower;
        mouseClickRight = Refresh;
        updateGhost = MoveGhostAfterCursor<TowerTile>;
    }

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClickLeft?.Invoke();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            mouseClickRight?.Invoke();
        }
        else if (currentGhost != null)
        {
            updateGhost?.Invoke();
        }
    }
    /// <summary>
    /// Removes a ghost from scene.
    /// </summary>
    private void ClearGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost.gameObject);
            currentGhost = null;
            updateGhost = null;
        }
    }
    private void Refresh()
    {
        ClearGhost();
        mouseClickLeft = null;
        mouseClickRight = null;
    }
    /// <summary>
    /// Places a unit on mouse cursor.
    /// </summary>
    private void PlaceUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SpawnTile tile = hit.collider.gameObject.GetComponentInParent<SpawnTile>();
            if (tile != null)
            {
                playerManager.SpawnUnit(tile);
                Refresh();
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(wrongPlace);
                }
            }
        }
    }
    /// <summary>
    /// Places a tower on mouse cursor.
    /// </summary>
    private void PlaceLaserTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        TowerTile tile;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if ((tile = hit.collider.gameObject.GetComponentInParent<TowerTile>()) != null)
            {
                if (!tile.IsOccupied)
                {
                    playerManager.SpawnLaserTower(tile);
                    Refresh();
                    return;
                }
            }
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(wrongPlace);
            }
        }
    }
    private void PlaceMGTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        TowerTile tile;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if ((tile = hit.collider.gameObject.GetComponentInParent<TowerTile>()) != null)
            {
                if (!tile.IsOccupied)
                {
                    playerManager.SpawnMGTower(tile);
                    Refresh();
                    return;
                }
            }
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(wrongPlace);
            }
        }
    }
    /// <summary>
    /// Makes current ghost object follow mouse cursor.
    /// </summary>
    /// <typeparam name="TargetTile"> Target tile type, on which we want our object to be placed. </typeparam>
    private void MoveGhostAfterCursor<TargetTile>() where TargetTile : Tile
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 1f is for sphere cast radius
        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, float.MaxValue, ghostWorldPlacementMask))
        {
            TargetTile tile = hit.collider.gameObject.GetComponentInParent<TargetTile>();
            if (tile != null)
            {
                currentGhost.transform.position = tile.transform.position;
                currentGhost.SetFit(true);
            }
            else
            {
                currentGhost.transform.position = hit.point;
                currentGhost.SetFit(false);
            }
        }
    }

    // DEBUG
    private void ShowCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.GetComponentInParent<Tile>()?.ToString());
        }
    }
}
