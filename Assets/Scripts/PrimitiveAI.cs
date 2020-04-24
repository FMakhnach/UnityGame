using System;
using UnityEngine;

public class PrimitiveAI : MonoBehaviour
{
    [SerializeField]
    private float money;
    [SerializeField]
    private float incomePerSecond;

    [SerializeField]
    private TowerFactory towerFactory;
    [SerializeField]
    private TowerPlacement[] towerPlacements;
    [SerializeField]
    private Tower[] towers;
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
    private Alignment alignment;
    private Action currentAction;

    private void Awake()
    {
        cooldown = 1f + UnityEngine.Random.value;
        alignment = Alignment.Computer;
        float modifier = 1 / (buggyProbability + copterProbability + laserProbability + mgProbability);
        buggyProbability *= modifier;
        copterProbability *= modifier;
        laserProbability *= modifier;
        mgProbability *= modifier;
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
        unitFactory.CreateBuggy().SpawnOn(spawns[id], alignment);
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
        unitFactory.CreateCopter().SpawnOn(spawns[id], alignment);
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
                towerFactory.CreateLaserTower().PlaceOn(
                    towerPlacements[i].transform.position,
                    towerPlacements[i].transform.rotation,
                    alignment);
                currentAction = null;
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
                towerFactory.CreateMGTower().PlaceOn(
                    towerPlacements[i].transform.position,
                    towerPlacements[i].transform.rotation,
                    alignment);
                currentAction = null;
            }
        }
    }
}
