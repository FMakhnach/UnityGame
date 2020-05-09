using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(AudioSource))]
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
    private Ghost buggyGhostPrefab;
    [SerializeField]
    private Ghost copterGhostPrefab;
    [SerializeField]
    private Ghost laserTurretGhostPrefab;
    [SerializeField]
    private Ghost mgTurretGhostPrefab;
    [SerializeField]
    private Ghost plantGhostPrefab;
    [SerializeField]
    private AudioClip wrongPlace;

    /// <summary>
    /// Current placeable object ghost in game.
    /// </summary>
    private Ghost currentGhost;
    private AudioSource audioSource;
    private EventSystem eventSystem;

    public void BuggyButtonClicked()
        => GameButtonClicked(Cost.Buggy, buggyGhostPrefab, PlaceBuggy);
    public void CopterButtonClicked()
        => GameButtonClicked(Cost.Copter, copterGhostPrefab, PlaceCopter);
    public void LaserTurretButtonClicked()
        => GameButtonClicked(Cost.LaserTurret, laserTurretGhostPrefab, PlaceLaserTurret);
    public void MGTurretButtonClicked()
        => GameButtonClicked(Cost.MachineGunTurret, mgTurretGhostPrefab, PlaceMGTurret);
    public void PlantButtonClicked()
        => GameButtonClicked(Cost.Plant, plantGhostPrefab, PlacePlant);

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
    }
    private void OnEnable()
    {
        ClearGhost();
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
    /// <summary>
    /// Clears ghost and mouse click actions.
    /// </summary>
    public void Refresh()
    {
        ClearGhost();
        mouseClickLeft = null;
        mouseClickRight = null;
    }

    private void PlaceBuggy()
    {
        if (currentGhost.IsFit)
        {
            playerManager.SpawnBuggy(currentGhost.PlaceArea as Spawn);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlaceCopter()
    {
        if (currentGhost.IsFit)
        {
            playerManager.SpawnCopter(currentGhost.PlaceArea as Spawn);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlaceLaserTurret()
    {
        if (currentGhost.IsFit)
        {
            playerManager.PlaceLaserTurret(currentGhost.PlaceArea as TurretPlacement);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlaceMGTurret()
    {
        if (currentGhost.IsFit)
        {
            playerManager.PlaceMGTurret(currentGhost.PlaceArea as TurretPlacement);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    private void PlacePlant()
    {
        if (currentGhost.IsFit)
        {
            playerManager.PlacePlant(currentGhost.PlaceArea as PlantPlacement);
            Refresh();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
    /// <summary>
    /// Just to avoid repeating code. Hope it's not overcomplicated.
    /// </summary>
    /// <param name="cost"> Cost of the object to buy. </param>
    /// <param name="ghostPrefab"> Object prefab. </param>
    /// <param name="placeMethod"> Method that places the object. </param>
    private void GameButtonClicked(float cost, Ghost ghostPrefab, Action placeMethod)
    {
        ClearGhost();
        if (playerManager.Energy >= cost)
        {
            currentGhost = Instantiate(ghostPrefab, Input.mousePosition, Quaternion.identity);
            currentGhost.Owner = playerManager;
            mouseClickLeft = placeMethod;
            mouseClickRight = Refresh;
        }
        else
        {
            audioSource.PlayOneShot(wrongPlace);
        }
    }
}