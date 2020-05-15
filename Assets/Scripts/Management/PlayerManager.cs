using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages players stuff. 
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public class Stats
    {
        public int UnitsKilled { get; private set; }
        public int TurretsKilled { get; private set; }
        public int UnitsLost { get; private set; }
        public int TurretsLost { get; private set; }
        public int MoneySpent { get; private set; }

        public Stats()
        {
            UnitsKilled = 0;
            TurretsKilled = 0;
            UnitsLost = 0;
            TurretsLost = 0;
            MoneySpent = 0;
        }

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
    /// The amount of money the player has at the beggining.
    /// </summary>
    [SerializeField]
    protected int startingEnergy;
    [SerializeField]
    private float incomePerSecond;
    /// <summary>
    /// The sum that the player can spend on buying stuff.
    /// </summary>
    private float energy;
    /// <summary>
    /// UI text element that shows current money to the player.
    /// </summary>
    [SerializeField]
    private TMP_Text energyText;
    [SerializeField]
    private TMP_Text energyIncomeText;

    /// <summary>
    /// The sum that the player can spend on buying stuff.
    /// </summary>
    public virtual float Energy
    {
        get => energy;
        protected set
        {
            energy = value;
            if (energyText != null)
            {
                energyText.text = ((int)energy).ToString();
            }
        }
    }
    public Stats PlayerStats { get; protected set; }

    protected virtual void Awake()
    {
        Energy = startingEnergy;
        PlayerStats = new Stats();
        energyIncomeText.text = incomePerSecond.ToString("0.##");
    }
    protected virtual void Start()
    {
        LevelManager.Instance.onGameStarted += () => gameObject.SetActive(true);
        LevelManager.Instance.onGameStarted += () => StartCoroutine("IncomeTick");
        gameObject.SetActive(false);
    }
    private IEnumerator IncomeTick()
    {
        for (; ; )
        {
            Energy += incomePerSecond;
            yield return new WaitForSeconds(1f);
        }
    }

    public void IncreaseIncome(float value)
    {
        incomePerSecond += value;
        if (energyIncomeText != null)
        {
            energyIncomeText.text = incomePerSecond.ToString("0.##");
        }
    }
    public void DecreaseIncome(float value)
    {
        if (value > incomePerSecond)
        {
            incomePerSecond = 0;
        }
        else
        {
            incomePerSecond -= value;
        }
        if (energyIncomeText != null)
        {
            energyIncomeText.text = incomePerSecond.ToString("0.##");
        }
    }
    public void SpendEnergy(int energy)
    {
        Energy -= energy;
        PlayerStats.SpendMoney(energy);
    }

    /// <summary>
    /// Creates a new instance of buggy and places on spawn.
    /// </summary>
    public Buggy SpawnBuggy(Spawn spawn)
    {
        SpendEnergy(Cost.Buggy);
        Buggy buggy = unitFactory.CreateBuggy(this);
        buggy.SpawnOn(spawn);
        buggy.gameObject.SetActive(true);
        buggy.InitializePath();
        return buggy;
    }
    /// <summary>
    /// Creates a new instance of copter and places on spawn.
    /// </summary>
    public Copter SpawnCopter(Spawn spawn)
    {
        SpendEnergy(Cost.Copter);
        Copter copter = unitFactory.CreateCopter(this);
        copter.SpawnOn(spawn);
        copter.gameObject.SetActive(true);
        copter.InitializePath();
        return copter;
    }
    /// <summary>
    /// Creates a new instance of laser turret and places on given area.
    /// </summary>
    public LaserTurret PlaceLaserTurret(TurretPlacement place)
    {
        SpendEnergy(Cost.LaserTurret);
        LaserTurret laser = turretFactory.CreateLaserTurret(this);
        laser.PlaceOn(place);
        laser.gameObject.SetActive(true);
        return laser;
    }
    /// <summary>
    /// Creates a new instance of machine gun turret and places on given area.
    /// </summary>
    public MachineGunTurret PlaceMGTurret(TurretPlacement place)
    {
        SpendEnergy(Cost.MachineGunTurret);
        MachineGunTurret mg = turretFactory.CreateMGTurret(this);
        mg.PlaceOn(place);
        mg.gameObject.SetActive(true);
        return mg;
    }
    /// <summary>
    /// Creates a new instance of plant and places on given area.
    /// </summary>
    public Plant PlacePlant(PlantPlacement place)
    {
        SpendEnergy(Cost.Plant);
        Plant plant = buildingFactory.CreatePlant(this);
        plant.PlaceOn(place);
        plant.gameObject.SetActive(true);
        return plant;
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