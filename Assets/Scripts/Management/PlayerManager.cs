using System.Collections;
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

    [SerializeField]
    private UnitFactory unitFactory;
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
    public Stats PlayerStats { get; private set; }

    private void Awake()
    {
        Money = startingMoney;
        incomePerSecond = 0;
        PlayerStats = new Stats();
        StartCoroutine("IncomeTick");
    }
    public void IncreaseIncome(float value)
    {
        incomePerSecond += value;
    }
    public void DecreaseIncome(float value)
    {
        incomePerSecond -= value;
    }
    public void SpendMoney(int money)
    {
        Money -= money;
        PlayerStats.SpendMoney(money);
    }
    private IEnumerator IncomeTick()
    {
        for (; ; )
        {
            Money += incomePerSecond;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Creates a new instance of buggy and places on spawn.
    /// </summary>
    public void SpawnBuggy(Spawn spawn)
    {
        unitFactory.CreateBuggy(this).SpawnOn(spawn);
        SpendMoney(Cost.Buggy);
    }
    /// <summary>
    /// Creates a new instance of copter and places on spawn.
    /// </summary>
    public void SpawnCopter(Spawn spawn)
    {
        unitFactory.CreateCopter(this).SpawnOn(spawn);
        SpendMoney(Cost.Copter);
    }
    /// <summary>
    /// Creates a new instance of laser turret and places on given area.
    /// </summary>
    public void PlaceLaserTurret(TurretPlacement place)
    {
        turretFactory.CreateLaserTurret(this).PlaceOn(place);
        SpendMoney(Cost.LaserTurret);
    }
    /// <summary>
    /// Creates a new instance of machine gun turret and places on given area.
    /// </summary>
    public void PlaceMGTurret(TurretPlacement place)
    {
        turretFactory.CreateMGTurret(this).PlaceOn(place);
        SpendMoney(Cost.MachineGunTurret);
    }
    /// <summary>
    /// Creates a new instance of plant and places on given area.
    /// </summary>
    public void PlacePlant(PlantPlacement place)
    {
        buildingFactory.CreatePlant(this).PlaceOn(place);
        SpendMoney(Cost.Plant);
    }

    /// <summary>
    /// Should be invoked on enemy unit killed.
    /// </summary>
    public void UnitKilled() => PlayerStats.UnitKilled();
    /// <summary>
    /// Should be invoked on enemy turret killed.
    /// </summary>
    public void TurretKilled() => PlayerStats.TurretKilled();
    /// <summary>
    /// Should be invoked on player unit lost.
    /// </summary>
    public void UnitLost() => PlayerStats.UnitLost();
    /// <summary>
    /// Should be invoked on player turret lost.
    /// </summary>
    public void TurretLost() => PlayerStats.TurretLost();
}