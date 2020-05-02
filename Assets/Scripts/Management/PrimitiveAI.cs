using System;
using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// My little AI.
/// Basically, what it does is spawning random game object 
/// each iteration (which is, for now, random value from 1 to 3 secs) if 
/// 1) it has enough money;
/// 2) it is possible (eg no place for turret).
/// Chances can be set manually, they are normalized automatically btw.
/// But its nice if you will make it valid from start.
/// </summary>
[RequireComponent(typeof(PlayerManager))]
public class PrimitiveAI : MonoBehaviour
{
    [SerializeField]
    private float money;
    [SerializeField]
    private float incomePerSecond;

    #region Turret stuff.
    [SerializeField]
    private TurretFactory turretFactory;
    [SerializeField]
    private TurretPlacement[] turretPlacements;
    [SerializeField]
    private int numberOfStartTurrets;
    private Turret[] turrets;
    [SerializeField]
    [Range(0, 1)]
    private float laserProbability;
    [SerializeField]
    [Range(0, 1)]
    private float mgProbability;
    #endregion
    #region Unit stuff.
    [SerializeField]
    private UnitFactory unitFactory;
    [SerializeField]
    private Spawn[] spawns;
    [SerializeField]
    [Range(0, 1)]
    private float buggyProbability;
    [SerializeField]
    [Range(0, 1)]
    private float copterProbability;
    #endregion
    #region Buildings stuff.
    [SerializeField]
    private BuildingFactory plantFactory;
    [SerializeField]
    private PlantPlacement[] plantPlacements;
    private Plant[] plants;
    [SerializeField]
    private int numberOfStartPlants;
    [SerializeField]
    [Range(0, 1)]
    private float plantProbability;
    #endregion

    private Action currentAction;
    private System.Random sysRand;
    private PlayerManager owner;

    private void Awake()
    {
        sysRand = new System.Random();
        owner = GetComponent<PlayerManager>();
        turrets = new Turret[turretPlacements.Length];
        plants = new Plant[plantPlacements.Length];
        numberOfStartTurrets = Math.Min(numberOfStartTurrets, turretPlacements.Length);
        numberOfStartPlants = Math.Min(numberOfStartPlants, plantPlacements.Length);

        // Normalizing probabilities (so they give 1 in sum).
        float modifier = 1 / (buggyProbability + copterProbability + laserProbability + mgProbability + plantProbability);
        buggyProbability *= modifier;
        copterProbability *= modifier;
        laserProbability *= modifier;
        mgProbability *= modifier;
        plantProbability *= modifier;

        float startMoney = money;
        GenerateStartTurrets();
        GenerateStartPlants();
        money = startMoney;
    }
    private void Start()
    {
        LevelManager.Instance.onGameStarted += LaunchAI;
    }
    private void LaunchAI()
    {
        StartCoroutine("IncomeTick");
        StartCoroutine("RandomAction");
    }
    /// <summary>
    /// Just gives income money every tick.
    /// </summary>
    private IEnumerator IncomeTick()
    {
        for (; ; )
        {
            money += incomePerSecond;
            yield return new WaitForSeconds(1f);
        }
    }
    /// <summary>
    /// Does random action every [1, 3] seconds.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RandomAction()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1f + UnityEngine.Random.value * 2f);
            if (currentAction == null)
            {
                currentAction = GetRandomAction();
            }
            currentAction();
        }
    }

    private void GenerateStartTurrets()
    {
        var startTurretPositions = Enumerable.Range(0, turretPlacements.Length).OrderBy(x => sysRand.Next()).ToArray();
        for (int i = 0; i < numberOfStartTurrets; i++)
        {
            int id = startTurretPositions[i];
            if (sysRand.Next(2) == 0)
            {
                turrets[id] = turretFactory.CreateLaserTurret(owner);
            }
            else
            {
                turrets[id] = turretFactory.CreateMGTurret(owner);
            }
            turrets[id].transform.position = turretPlacements[id].transform.position;
            turrets[id].transform.rotation = turretPlacements[id].Rotation;
        }
    }
    private void GenerateStartPlants()
    {
        for (int i = 0; i < numberOfStartPlants; i++)
        {
            plants[i] = plantFactory.CreatePlant(owner);
            plants[i].transform.position = plantPlacements[i].transform.position;
            plants[i].transform.rotation = plantPlacements[i].Rotation;
        }
    }
    private Action GetRandomAction()
    {
        float chance = UnityEngine.Random.value;
        if (chance < buggyProbability)
        {
            return SpawnBuggy;
        }
        chance -= buggyProbability;
        if (chance < copterProbability)
        {
            return SpawnCopter;
        }
        chance -= copterProbability;
        if (chance < laserProbability)
        {
            return PlaceLaser;
        }
        if (chance < mgProbability)
        {
            return PlaceMG;
        }
        return PlacePlant;
    }
    private void SpawnBuggy()
    {
        if (money < Cost.Buggy)
        {
            return;
        }
        int id = (int)(UnityEngine.Random.value * spawns.Length);
        if (id == spawns.Length)
        {
            id--;
        }
        money -= Cost.Buggy;
        unitFactory.CreateBuggy(owner).SpawnOn(spawns[id]);
        currentAction = null;
    }
    private void SpawnCopter()
    {
        if (money < Cost.Copter)
        {
            return;
        }
        int id = (int)(UnityEngine.Random.value * spawns.Length);
        if (id == spawns.Length)
        {
            id--;
        }
        money -= Cost.Copter;
        unitFactory.CreateCopter(owner).SpawnOn(spawns[id]);
        currentAction = null;
    }
    private void PlaceLaser()
    {
        if (money < Cost.LaserTurret)
        {
            return;
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] == null || turrets[i].ToString() == "null")
            {
                money -= Cost.LaserTurret;
                turrets[i] = turretFactory.CreateLaserTurret(owner);
                turrets[i].PlaceOn(turretPlacements[i]);
                currentAction = null;
                return;
            }
        }
        currentAction = null;
    }
    private void PlaceMG()
    {
        if (money < Cost.MachineGunTurret)
        {
            return;
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] == null || turrets[i].ToString() == "null")
            {
                money -= Cost.MachineGunTurret;
                turrets[i] = turretFactory.CreateMGTurret(owner);
                turrets[i].PlaceOn(turretPlacements[i]);
                currentAction = null;
                return;
            }
        }
        currentAction = null;
    }
    private void PlacePlant()
    {
        if (money < Cost.Plant)
        {
            return;
        }
        for (int i = 0; i < plants.Length; i++)
        {
            if (plants[i] == null || plants[i].ToString() == "null")
            {
                money -= Cost.Plant;
                plants[i] = plantFactory.CreatePlant(owner);
                plants[i].PlaceOn(plantPlacements[i]);
                currentAction = null;
                return;
            }
        }
        currentAction = null;
    }
}
