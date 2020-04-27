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
    private Tower.Factory towerFactory;
    [SerializeField]
    private TowerPlacement[] towerPlacements;
    [SerializeField]
    private int numberOfStartTowers;
    private Tower[] towers;
    [SerializeField]
    [Range(0, 1)]
    private float laserProbability;
    [SerializeField]
    [Range(0, 1)]
    private float mgProbability;

    [SerializeField]
    private Unit.Factory unitFactory;
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
        towers = new Tower[towerPlacements.Length];
        numberOfStartTowers = Math.Min(numberOfStartTowers, towerPlacements.Length);

        cooldown = 2f + UnityEngine.Random.value;
        float modifier = 1 / (buggyProbability + copterProbability + laserProbability + mgProbability);
        buggyProbability *= modifier;
        copterProbability *= modifier;
        laserProbability *= modifier;
        mgProbability *= modifier;

        float startMoney = money;
        GenerateStartTowers();
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
    private void GenerateStartTowers()
    {
        var startTowerPositions = Enumerable.Range(0, towerPlacements.Length).OrderBy(x => sysRand.Next()).ToArray();
        for (int i = 0; i < numberOfStartTowers; i++)
        {
            int id = startTowerPositions[i];
            if (sysRand.Next(2) == 0)
            {
                towers[id] = towerFactory.CreateLaserTower(owner);
            }
            else
            {
                towers[id] = towerFactory.CreateMGTower(owner);
            }
            towers[id].PlaceOn(towerPlacements[id]);
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
        if (money < LaserTower.Cost)
        {
            return;
        }
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] == null)
            {
                money -= LaserTower.Cost;
                towers[i] = towerFactory.CreateLaserTower(owner);
                towers[i].PlaceOn(towerPlacements[i]);
                currentAction = null;
                return;
            }
        }
    }
    private void PlaceMG()
    {
        if (money < MachineGunTower.Cost)
        {
            return;
        }
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] == null)
            {
                money -= MachineGunTower.Cost;
                towers[i] = towerFactory.CreateMGTower(owner);
                towers[i].PlaceOn(towerPlacements[i]);
                currentAction = null;
                return;
            }
        }
    }
}
