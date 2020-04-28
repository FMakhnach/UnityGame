using System;
using System.Linq;
using UnityEngine;

public class PrimitiveAI : MonoBehaviour
{
    [SerializeField]
    private float money;
    [SerializeField]
    private float incomePerSecond;

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

    private float cooldown;
    private Action currentAction;
    private System.Random sysRand;
    private PlayerManager owner;

    private void Awake()
    {
        sysRand = new System.Random();
        owner = GetComponent<PlayerManager>();
        turrets = new Turret[turretPlacements.Length];
        numberOfStartTurrets = Math.Min(numberOfStartTurrets, turretPlacements.Length);

        cooldown = 2f + UnityEngine.Random.value;
        float modifier = 1 / (buggyProbability + copterProbability + laserProbability + mgProbability);
        buggyProbability *= modifier;
        copterProbability *= modifier;
        laserProbability *= modifier;
        mgProbability *= modifier;

        float startMoney = money;
        GenerateStartTurrets();
        money = startMoney;
    }
    private void Update()
    {
        money += incomePerSecond * Time.deltaTime;
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (currentAction == null)
            {
                currentAction = GetRandomAction();
            }
            currentAction();
            cooldown = 1f + UnityEngine.Random.value;
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
        return PlaceMG;
    }
    private void SpawnBuggy()
    {
        if (money < Buggy.Cost)
        {
            return;
        }
        int id = (int)(UnityEngine.Random.value * spawns.Length);
        if (id == spawns.Length)
        {
            id--;
        }
        money -= Buggy.Cost;
        unitFactory.CreateBuggy(owner).SpawnOn(spawns[id]);
        currentAction = null;
    }
    private void SpawnCopter()
    {
        if (money < Copter.Cost)
        {
            return;
        }
        int id = (int)(UnityEngine.Random.value * spawns.Length);
        if (id == spawns.Length)
        {
            id--;
        }
        money -= Copter.Cost;
        unitFactory.CreateCopter(owner).SpawnOn(spawns[id]);
        currentAction = null;
    }
    private void PlaceLaser()
    {
        if (money < LaserTurret.Cost)
        {
            return;
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] == null)
            {
                money -= LaserTurret.Cost;
                turrets[i] = turretFactory.CreateLaserTurret(owner);
                turrets[i].PlaceOn(turretPlacements[i]);
                currentAction = null;
                return;
            }
        }
    }
    private void PlaceMG()
    {
        if (money < MachineGunTurret.Cost)
        {
            return;
        }
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] == null)
            {
                money -= MachineGunTurret.Cost;
                turrets[i] = turretFactory.CreateMGTurret(owner);
                turrets[i].PlaceOn(turretPlacements[i]);
                currentAction = null;
                return;
            }
        }
    }
}
