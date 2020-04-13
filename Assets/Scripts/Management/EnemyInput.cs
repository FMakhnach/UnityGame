using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Dummy class for spawning enemies (debug and test mostly).
/// </summary>
public class EnemyInput : MonoBehaviour
{
    private PlayerManager enemyManager;
    /// <summary>
    /// Need it to avoid two ghosts at the same time.
    /// </summary>
    [SerializeField]
    private InputManager playerInput;
    [SerializeField]
    private Toggle northRoad;
    [SerializeField]
    private Toggle middleRoad;
    [SerializeField]
    private Toggle southRoad;
    [SerializeField]
    private Spawn northSpawn;
    [SerializeField]
    private Spawn middleSpawn;
    [SerializeField]
    private Spawn southSpawn;

    public void SpawnBuggy()
    {
        Refresh();
        if (northRoad.isOn)
        {
            enemyManager.SpawnBuggy(northSpawn);
        }
        else if (middleRoad.isOn)
        {
            enemyManager.SpawnBuggy(middleSpawn);
        }
        else if (southRoad.isOn)
        {
            enemyManager.SpawnBuggy(southSpawn);
        }
    }
    public void SpawnCopter()
    {
        Refresh();
        if (northRoad.isOn)
        {
            enemyManager.SpawnCopter(northSpawn);
        }
        else if (middleRoad.isOn)
        {
            enemyManager.SpawnCopter(middleSpawn);
        }
        else if (southRoad.isOn)
        {
            enemyManager.SpawnCopter(southSpawn);
        }
    }

    #region code from input manager
    private LayerMask ghostWorldPlacementMask;
    private Ghost currentGhost;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip wrongPlace;
    private Action mouseClickLeft;
    private Action mouseClickRight;
    [SerializeField]
    private Ghost laserTowerGhostPrefab;
    [SerializeField]
    private Ghost mgTowerGhostPrefab;
    private void Awake()
    {
        enemyManager = GetComponent<PlayerManager>();
        ghostWorldPlacementMask = LayerMask.GetMask("Environment", "TowerPlacement", "UnitPlacement");
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
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
    public void LaserTowerButtonClicked()
    {
        ClearGhost();
        playerInput.Refresh();
        if (enemyManager.Currency >= LaserTower.Cost)
        {
            currentGhost = Instantiate(laserTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = enemyManager.Alignment;
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
        playerInput.Refresh();
        if (enemyManager.Currency >= MachineGunTower.Cost)
        {
            currentGhost = Instantiate(mgTowerGhostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Alignment = enemyManager.Alignment;
            mouseClickLeft = PlaceMGTower;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlaceLaserTower()
    => Place(enemyManager.PlaceLaserTower);
    private void PlaceMGTower()
        => Place(enemyManager.PlaceMGTower);
    private void MoveGhostAfterCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 1f is for sphere cast radius
        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, float.MaxValue, ghostWorldPlacementMask))
        {
            currentGhost.transform.position = hit.point;
            currentGhost.transform.rotation
                = Quaternion.FromToRotation(Vector3.up, hit.normal);
            currentGhost.CheckIfFits();
        }
    }
    private void Place(Action<Vector3, Quaternion> placingMethod)
    {
        if (currentGhost.IsFit)
        {
            placingMethod(currentGhost.transform.position, currentGhost.transform.rotation);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    #endregion
}
