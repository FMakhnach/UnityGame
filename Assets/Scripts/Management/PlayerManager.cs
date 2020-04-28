using TMPro;
using UnityEngine;

/// <summary>
/// Manages players stuff. 
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public struct Stats
    {
        public int UnitsKilled { get; private set; }
        public int TurretsKilled { get; private set; }
        public int UnitsLost { get; private set; }
        public int TurretsLost { get; private set; }
        public int MoneySpent { get; private set; }

        public void UnitKilled() => UnitsKilled++;
        public void TurretKilled() => TurretsKilled++;
        public void UnitLost() => UnitsLost++;
        public void TurretLost() => TurretsLost++;
        public void SpendMoney(int money) => MoneySpent += money;
    }

    /// <summary>
    /// The object that is responsible for creating units. 
    /// </summary>
    [SerializeField]
    private UnitFactory unitFactory;
    /// <summary>
    /// The object that is responsible for creating units. 
    /// </summary>
    [SerializeField]
    private TurretFactory turretFactory;
    [SerializeField]
    private BuildingFactory buildingFactory;
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
    private Stats stats;

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
    public Stats Stat => stats;

    private void Awake()
    {
        Money = startingMoney;
        incomePerSecond = 0;
        stats = new Stats();
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
        stats.SpendMoney(Buggy.Cost);
    }
    /// <summary>
    /// Creates a new instance of copter and places on spawn.
    /// </summary>
    public void SpawnCopter(Spawn spawn)
    {
        unitFactory.CreateCopter(this).SpawnOn(spawn);
        Money -= Copter.Cost;
        stats.SpendMoney(Copter.Cost);
    }
    /// <summary>
    /// Creates a new instance of laser turret and places on given point.
    /// </summary>
    public void PlaceLaserTurret(TurretPlacement place)
    {
        turretFactory.CreateLaserTurret(this).PlaceOn(place);
        Money -= LaserTurret.Cost;
        stats.SpendMoney(LaserTurret.Cost);
    }
    /// <summary>
    /// Creates a new instance of machine gun turret and places on given point.
    /// </summary>
    public void PlaceMGTurret(TurretPlacement place)
    {
        turretFactory.CreateMGTurret(this).PlaceOn(place);
        Money -= MachineGunTurret.Cost;
        stats.SpendMoney(MachineGunTurret.Cost);
    }
    public void PlacePlant(PlantPlacement place)
    {
        buildingFactory.CreatePlant(this).PlaceOn(place, this);
        Money -= Plant.Cost;
        stats.SpendMoney(Plant.Cost);
    }
    public void UnitKilled() => stats.UnitKilled();
    public void TurretKilled() => stats.TurretKilled();
    public void UnitLost() => stats.UnitLost();
    public void TurretLost() => stats.TurretLost();
    public void SpendMoney(int money) => stats.SpendMoney(money);
}