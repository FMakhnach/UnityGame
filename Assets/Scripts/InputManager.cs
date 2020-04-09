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
    private Ghost buggyGhostPrefab;
    [SerializeField]
    private Ghost copterGhostPrefab;
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
    private Tile currentTile;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip wrongPlace;

    public void BuggyButtonClicked()
    {
        ClearGhost();
        currentGhost = Instantiate(buggyGhostPrefab, Input.mousePosition, Quaternion.identity);
        mouseClickLeft = PlaceBuggy;
        mouseClickRight = Refresh;
        updateGhost = MoveGhostAfterCursor<SpawnTile>;
    }
    public void CopterButtonClicked()
    {
        ClearGhost();
        currentGhost = Instantiate(copterGhostPrefab, Input.mousePosition, Quaternion.identity);
        mouseClickLeft = PlaceCopter;
        mouseClickRight = Refresh;
        updateGhost = MoveGhostAfterCursor<SpawnTile>;
    }
    public void LaserTowerButtonClicked()
    {
        ClearGhost();
        if(playerManager.Currency >= LaserTower.Cost)
        {
            currentGhost = Instantiate(laserTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
            mouseClickLeft = PlaceLaserTower;
            mouseClickRight = Refresh;
            updateGhost = MoveGhostAfterCursor<TowerTile>;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    public void MGTowerButtonClicked()
    {
        ClearGhost();
        if(playerManager.Currency >= MachineGunTower.Cost)
        {
            currentGhost = Instantiate(mgTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
            mouseClickLeft = PlaceMGTower;
            mouseClickRight = Refresh;
            updateGhost = MoveGhostAfterCursor<TowerTile>;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
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
        currentTile = null;
    }
    private void PlaceBuggy()
        => PlaceUnit(playerManager.SpawnBuggy);
    private void PlaceCopter()
        => PlaceUnit(playerManager.SpawnCopter);
    private void PlaceLaserTower()
        => PlaceTower(playerManager.PlaceLaserTower);
    private void PlaceMGTower()
        => PlaceTower(playerManager.PlaceMGTower);
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
            currentTile = hit.collider.gameObject.GetComponentInParent<TargetTile>();
            if (currentTile != null)
            {
                currentGhost.transform.position = currentTile.transform.position;
                currentGhost.SetFit(true);
            }
            else
            {
                currentGhost.transform.position = hit.point;
                currentGhost.SetFit(false);
            }
        }
    }

    private void PlaceUnit(Action<SpawnTile> placingMethod)
    {
        if(currentTile == null)
        {
            return;
        }

        if (currentTile is SpawnTile)
        {
            placingMethod(currentTile as SpawnTile);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlaceTower(Action<TowerTile> placingMethod)
    {
        if (currentTile == null)
        {
            return;
        }

        if (currentTile is TowerTile)
        {
            TowerTile tile = currentTile as TowerTile;
            if (!tile.IsOccupied)
            {
                placingMethod(tile);
                Refresh();
                return;
            }
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }

    // DEBUG
    private void ShowCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.transform.parent.position);
        }
    }
}
