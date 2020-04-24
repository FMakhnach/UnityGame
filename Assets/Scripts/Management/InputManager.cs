using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// Handles entities creating, so we need it.
    /// </summary>
    private PlayerManager playerManager;
    /// <summary>
    /// Action that should be happened on left mouse click.
    /// </summary>
    private Action mouseClickLeft;
    /// <summary>
    /// Action that should be happened on left mouse click.
    /// </summary>
    private Action mouseClickRight;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Ghost buggyGhostPrefab;
    [SerializeField]
    private Ghost copterGhostPrefab;
    [SerializeField]
    private Ghost laserTowerGhostPrefab;
    [SerializeField]
    private Ghost mgTowerGhostPrefab;
    [SerializeField]
    private Ghost plantGhostPrefab;
    /// <summary>
    /// The layer that the ghost floats on.
    /// </summary>
    [SerializeField]
    private LayerMask ghostWorldPlacementMask;
    [SerializeField]
    private AudioClip wrongPlace;

    /// <summary>
    /// Current placeable object ghost in game.
    /// </summary>
    private Ghost currentGhost;
    private AudioSource audioSource;
    private EventSystem eventSystem;

    /// <summary>
    /// Need it to avoid two ghosts at the same time.
    /// </summary>
    [SerializeField]
    private EnemyInput enemyInput;

    public void BuggyButtonClicked()
    {
        ClearGhost();
        enemyInput.Refresh();
        if (playerManager.Money >= Buggy.Cost)
        {
            currentGhost = Instantiate(buggyGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = playerManager.Alignment;
            mouseClickLeft = PlaceBuggy;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    public void CopterButtonClicked()
    {
        ClearGhost();
        enemyInput.Refresh();
        if (playerManager.Money >= Copter.Cost)
        {
            currentGhost = Instantiate(copterGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = playerManager.Alignment;
            mouseClickLeft = PlaceCopter;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    public void LaserTowerButtonClicked()
    {
        ClearGhost();
        enemyInput.Refresh();
        if (playerManager.Money >= LaserTower.Cost)
        {
            currentGhost = Instantiate(laserTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = playerManager.Alignment;
            mouseClickLeft = PlaceLaserTower;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    public void MGTowerButtonClicked()
    {
        ClearGhost();
        enemyInput.Refresh();
        if (playerManager.Money >= MachineGunTower.Cost)
        {
            currentGhost = Instantiate(mgTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = playerManager.Alignment;
            mouseClickLeft = PlaceMGTower;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    public void PlantButtonClicked()
    {
        ClearGhost();
        enemyInput.Refresh();
        if (playerManager.Money >= Plant.Cost)
        {
            currentGhost = Instantiate(plantGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = playerManager.Alignment;
            mouseClickLeft = PlacePlant;
            mouseClickRight = Refresh;
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
        eventSystem = EventSystem.current;
    }
    private void Update()
    {
        if (eventSystem.IsPointerOverGameObject())
        {
            return;
        }

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
            MoveGhostAfterCursor();
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
        }
    }
    public void Refresh()
    {
        ClearGhost();
        mouseClickLeft = null;
        mouseClickRight = null;
    }

    private void PlaceBuggy()
        => Place<Spawn>(playerManager.SpawnBuggy);
    private void PlaceCopter()
        => Place<Spawn>(playerManager.SpawnCopter);
    private void PlaceLaserTower()
        => Place<TowerPlacement>(playerManager.PlaceLaserTower);
    private void PlaceMGTower()
        => Place<TowerPlacement>(playerManager.PlaceMGTower);
    private void PlacePlant()
        => Place<PlantPlacement>(playerManager.PlacePlant);

    /// <summary>
    /// Makes current ghost object follow mouse cursor.
    /// </summary>
    /// <typeparam name="TargetTile"> Target tile type, on which we want our object to be placed. </typeparam>
    private void MoveGhostAfterCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // 1f is for sphere cast radius
        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, 1000f, ghostWorldPlacementMask))
        {
            currentGhost.transform.position = hit.point;
            currentGhost.transform.rotation
                = Quaternion.FromToRotation(Vector3.up, hit.normal);
            currentGhost.CheckIfFits();
        }
    }

    /// <summary>
    /// Places a particular tower.
    /// </summary>
    /// <param name="placingMethod"></param>
    private void Place<T>(Action<T> placingMethod) where T : PlaceArea
    {
        if (currentGhost.IsFit)
        {
            placingMethod(currentGhost.PlaceArea as T);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
}