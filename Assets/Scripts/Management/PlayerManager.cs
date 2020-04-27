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
    private Unit.Factory unitFactory;
    /// <summary>
    /// The object that is responsible for creating units. 
    /// </summary>
    [SerializeField]
    private Tower.Factory towerFactory;
    [SerializeField]
    private Plant.Factory plantFactory;
    /// <summary>
    /// UI text element that shows current money to the player.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI moneyText;
    /// <summary>
    /// The amount of money the player has at the beggining.
    /// </summary>
    [SerializeField]
    private int startingMoney;
    /// <summary>
    /// The sum that the player can spend on buying stuff.
    /// </summary>
    private float money;
    private float incomePerSecond;

    /// <summary>
    /// The sum that the player can spend on buying stuff.
    /// </summary>
    public float Money
    {
        get => money;
        private set
        {
            Debug.Assert(value >= 0, $"Putting negative {value} to money!");
            money = value;
            if (moneyText != null)
            {
                moneyText.text = ((int)money).ToString();
            }
        }
    }

    private void Awake()
    {
        Money = startingMoney;
        incomePerSecond = 0;
    }
    private void Update()
    {
        Money += incomePerSecond * Time.deltaTime;
    }
    public void IncreaseIncome(float value)
    {
        incomePerSecond += value;
    }
    public void DecreaseIncome(float value)
    {
        incomePerSecond -= value;
    }

    /// <summary>
    /// Creates a new instance of buggy and places on spawn.
    /// </summary>
    public void SpawnBuggy(Spawn spawn)
    {
        unitFactory.CreateBuggy(this).SpawnOn(spawn);
        Money -= Buggy.Cost;
    }
    /// <summary>
    /// Creates a new instance of copter and places on spawn.
    /// </summary>
    public void SpawnCopter(Spawn spawn)
    {
        unitFactory.CreateCopter(this).SpawnOn(spawn);
        Money -= Copter.Cost;
    }
    /// <summary>
    /// Creates a new instance of laser tower and places on given point.
    /// </summary>
    public void PlaceLaserTower(TowerPlacement place)
    {
        towerFactory.CreateLaserTower(this).PlaceOn(place);
        Money -= LaserTower.Cost;
    }
    /// <summary>
    /// Creates a new instance of machine gun tower and places on given point.
    /// </summary>
    public void PlaceMGTower(TowerPlacement place)
    {
        towerFactory.CreateMGTower(this).PlaceOn(place);
        Money -= MachineGunTower.Cost;
    }
    public void PlacePlant(PlantPlacement place)
    {
        plantFactory.CreatePlant(this).PlaceOn(place, this);
        Money -= Plant.Cost;
    }
}