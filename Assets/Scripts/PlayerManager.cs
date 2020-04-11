using TMPro;
using UnityEngine;

/// <summary>
/// Manages players stuff. 
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// The object that is responsible for creating units. 
    /// </summary>
    [SerializeField]
    private UnitFactory unitFactory;
    /// <summary>
    /// The object that is responsible for creating units. 
    /// </summary>
    [SerializeField]
    private TowerFactory towerFactory;
    /// <summary>
    /// The "team" of the player.
    /// </summary>
    [SerializeField]
    private Alignment alignment;
    /// <summary>
    /// UI text element that shows current money to the player.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI currencyText;
    /// <summary>
    /// The amount of money the player has at the beggining.
    /// </summary>
    [SerializeField]
    private int startingCurrency;
    /// <summary>
    /// The sum that the player can spend on buying stuff.
    /// </summary>
    private int currency;

    /// <summary>
    /// The sum that the player can spend on buying stuff.
    /// </summary>
    public int Currency
    {
        get => currency;
        private set
        {
            Debug.Assert(value < 0, "Trying to put negative to currency!");
            currency = value;
            currencyText.text = Currency.ToString();
        }
    }

    private void Awake()
    {
        Currency = startingCurrency;
    }

    /// <summary>
    /// Creates a new instance of buggy and places on spawn.
    /// </summary>
    public void SpawnBuggy(Spawn spawn)
    {
        unitFactory.CreateBuggy().SpawnOn(spawn, Alignment.Computer);
    }
    /// <summary>
    /// Creates a new instance of copter and places on spawn.
    /// </summary>
    public void SpawnCopter(Spawn spawn)
    {
        unitFactory.CreateCopter().SpawnOn(spawn, Alignment.Computer);
    }
    /// <summary>
    /// Creates a new instance of laser tower and places on given point.
    /// </summary>
    public void PlaceLaserTower(Vector3 placePoint, Quaternion rotation)
    {
        towerFactory.CreateLaserTower().PlaceOn(placePoint, rotation, alignment);
        Currency -= LaserTower.Cost;
    }
    /// <summary>
    /// Creates a new instance of machine gun tower and places on given point.
    /// </summary>
    public void PlaceMGTower(Vector3 placePoint, Quaternion rotation)
    {
        towerFactory.CreateMGTower().PlaceOn(placePoint, rotation, alignment);
        Currency -= MachineGunTower.Cost;
    }
}
