using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private Toggle westRoad;
    [SerializeField]
    private Toggle eastRoad;
    [SerializeField]
    private Spawn westSpawn;
    [SerializeField]
    private Spawn eastSpawn;

    /// <summary>
    /// To not use Camera.main
    /// </summary>
    [SerializeField]
    private Camera mainCamera;

    public void SpawnBuggy()
    {
        Refresh();
        if (westRoad.isOn)
        {
            enemyManager.SpawnBuggy(westSpawn);
        }
        else if (eastRoad.isOn)
        {
            enemyManager.SpawnBuggy(eastSpawn);
        }
    }
    public void SpawnCopter()
    {
        Refresh();
        if (westRoad.isOn)
        {
            enemyManager.SpawnCopter(westSpawn);
        }
        else if (eastRoad.isOn)
        {
            enemyManager.SpawnCopter(eastSpawn);
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
    private Ghost laserTurretGhostPrefab;
    [SerializeField]
    private Ghost mgTurretGhostPrefab;
    private void Awake()
    {
        enemyManager = GetComponent<PlayerManager>();
        ghostWorldPlacementMask = LayerMask.GetMask("Environment", "TurretPlacement", "UnitPlacement");
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
    public void LaserTurretButtonClicked()
    {
        ClearGhost();
        playerInput.Refresh();
        if (enemyManager.Money >= Cost.LaserTurret)
        {
            currentGhost = Instantiate(laserTurretGhostPrefab, Input.mousePosition, Quaternion.identity);
            //currentGhost.Alignment = enemyManager.Alignment;
            mouseClickLeft = PlaceLaserTurret;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    public void MGTurretButtonClicked()
    {
        ClearGhost();
        playerInput.Refresh();
        if (enemyManager.Money >= Cost.MachineGunTurret)
        {
            currentGhost = Instantiate(mgTurretGhostPrefab, Input.mousePosition, Quaternion.identity);
            //currentGhost.Alignment = enemyManager.Alignment;
            mouseClickLeft = PlaceMGTurret;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlaceLaserTurret()
        => Place(enemyManager.PlaceLaserTurret);
    private void PlaceMGTurret()
        => Place(enemyManager.PlaceMGTurret);
    private void MoveGhostAfterCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // 1f is for sphere cast radius
        if (Physics.SphereCast(ray, 1f, out RaycastHit hit, float.MaxValue, ghostWorldPlacementMask))
        {
            currentGhost.transform.position = hit.point;
            currentGhost.transform.rotation
                = Quaternion.FromToRotation(Vector3.up, hit.normal);
            currentGhost.CheckIfFits();
        }
    }
    private void Place(Action<TurretPlacement> placingMethod)
    {
        if (currentGhost.IsFit)
        {
            placingMethod(currentGhost.PlaceArea as TurretPlacement);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    #endregion
}