using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    #region Objects
    [SerializeField]
    private Buggy buggyPrefab;
    [SerializeField]
    private Copter copterPrefab;
    [SerializeField]
    private MachineGunTurret mgPrefab;
    [SerializeField]
    private LaserTurret laserPrefab;
    [SerializeField]
    private Plant plantPrefab;

    [SerializeField]
    private GameObject buggiesParent;
    [SerializeField]
    private GameObject coptersParent;
    [SerializeField]
    private GameObject lasersParent;
    [SerializeField]
    private GameObject mgsParent;
    [SerializeField]
    private GameObject plantsParent;

    [SerializeField]
    private int numberOfBuggies;
    [SerializeField]
    private int numberOfCopters;
    [SerializeField]
    private int numberOfMachineGuns;
    [SerializeField]
    private int numberOfLasers;
    [SerializeField]
    private int numberOfPlants;

    private List<Buggy> buggies;
    private List<Copter> copters;
    private List<MachineGunTurret> machineGuns;
    private List<LaserTurret> lasers;
    private List<Plant> plants;
    #endregion

    #region Projectiles
    [SerializeField]
    private Projectile buggyProjectilePrefab;
    [SerializeField]
    private Projectile copterProjectilePrefab;
    [SerializeField]
    private Projectile mgProjectilePrefab;
    [SerializeField]
    private Projectile laserProjectilePrefab;

    [SerializeField]
    private GameObject buggyProjectilesParent;
    [SerializeField]
    private GameObject copterProjectilesParent;
    [SerializeField]
    private GameObject mgProjectilesParent;
    [SerializeField]
    private GameObject laserProjectilesParent;

    [SerializeField]
    private int numberOfBuggyProjectiles;
    [SerializeField]
    private int numberOfCopterProjectiles;
    [SerializeField]
    private int numberOfMGProjectiles;
    [SerializeField]
    private int numberOfLaserProjectiles;

    private List<Projectile> buggyProjectiles;
    private List<Projectile> copterProjectiles;
    private List<Projectile> mgProjectiles;
    private List<Projectile> laserProjectiles;
    #endregion

    #region Particles
    [SerializeField]
    private ParticleSystem smallExplosionPrefab;
    [SerializeField]
    private ParticleSystem turretExplosionPrefab;
    [SerializeField]
    private ParticleSystem bigExplosionPrefab;
    [SerializeField]
    private ParticleSystem enemyShootPrefab;
    [SerializeField]
    private ParticleSystem laserShootPrefab;
    [SerializeField]
    private ParticleSystem mgShootPrefab;

    [SerializeField]
    private GameObject particlesParent;

    [SerializeField]
    private int numberOfSmallExplosions;
    [SerializeField]
    private int numberOfTurretExplosions;
    [SerializeField]
    private int numberOfBigExplosions;
    [SerializeField]
    private int numberOfEnemyShootEffects;
    [SerializeField]
    private int numberOfLaserShootEffects;
    [SerializeField]
    private int numberOfMGShootEffects;

    private List<ParticleSystem> smallExplosions;
    private List<ParticleSystem> turretExplosions;
    private List<ParticleSystem> bigExplosions;
    private List<ParticleSystem> enemyShootEffects;
    private List<ParticleSystem> laserShootEffects;
    private List<ParticleSystem> mgShootEffects;
    #endregion

    #region Ghosts
    [SerializeField]
    private Ghost buggyGhostPrefab;
    [SerializeField]
    private Ghost copterGhostPrefab;
    [SerializeField]
    private Ghost mgGhostPrefab;
    [SerializeField]
    private Ghost laserGhostPrefab;
    [SerializeField]
    private Ghost plantGhostPrefab;

    [SerializeField]
    private GameObject ghostsParent;

    public Ghost BuggyGhost { get; private set; }
    public Ghost CopterGhost { get; private set; }
    public Ghost MGTurretGhost { get; private set; }
    public Ghost LaserTurretGhost { get; private set; }
    public Ghost PlantGhost { get; private set; }
    #endregion

    private Vector3 storingPosition;

    public event Action onInitialized;

    public void Initialize()
    {
        #region Objects
        buggies = new List<Buggy>(numberOfBuggies);
        for (int i = 0; i < numberOfBuggies; i++)
        {
            buggies.Add(Instantiate(buggyPrefab, storingPosition,
                Quaternion.identity, buggiesParent.transform));
            buggies[i].gameObject.SetActive(false);
        }

        copters = new List<Copter>(numberOfCopters);
        for (int i = 0; i < numberOfCopters; i++)
        {
            copters.Add(Instantiate(copterPrefab, storingPosition,
                Quaternion.identity, coptersParent.transform));
            copters[i].gameObject.SetActive(false);
        }

        machineGuns = new List<MachineGunTurret>(numberOfMachineGuns);
        for (int i = 0; i < numberOfMachineGuns; i++)
        {
            machineGuns.Add(Instantiate(mgPrefab, storingPosition,
                Quaternion.identity, mgsParent.transform));
            machineGuns[i].gameObject.SetActive(false);
        }

        lasers = new List<LaserTurret>(numberOfLasers);
        for (int i = 0; i < numberOfLasers; i++)
        {
            lasers.Add(Instantiate(laserPrefab, storingPosition,
                Quaternion.identity, lasersParent.transform));
            lasers[i].gameObject.SetActive(false);
        }

        plants = new List<Plant>(numberOfPlants);
        for (int i = 0; i < numberOfPlants; i++)
        {
            plants.Add(Instantiate(plantPrefab, storingPosition,
                Quaternion.identity, plantsParent.transform));
            plants[i].gameObject.SetActive(false);
        }
        #endregion

        #region Projectile
        buggyProjectiles = new List<Projectile>(numberOfBuggyProjectiles);
        for (int i = 0; i < numberOfBuggyProjectiles; i++)
        {
            buggyProjectiles.Add(Instantiate(buggyProjectilePrefab, storingPosition,
                Quaternion.identity, buggyProjectilesParent.transform));
            buggyProjectiles[i].gameObject.SetActive(false);
        }

        copterProjectiles = new List<Projectile>(numberOfCopterProjectiles);
        for (int i = 0; i < numberOfCopterProjectiles; i++)
        {
            copterProjectiles.Add(Instantiate(copterProjectilePrefab, storingPosition,
                Quaternion.identity, copterProjectilesParent.transform));
            copterProjectiles[i].gameObject.SetActive(false);
        }

        mgProjectiles = new List<Projectile>(numberOfMGProjectiles);
        for (int i = 0; i < numberOfMGProjectiles; i++)
        {
            mgProjectiles.Add(Instantiate(mgProjectilePrefab, storingPosition,
                Quaternion.identity, mgProjectilesParent.transform));
            mgProjectiles[i].gameObject.SetActive(false);
        }

        laserProjectiles = new List<Projectile>(numberOfLaserProjectiles);
        for (int i = 0; i < numberOfLaserProjectiles; i++)
        {
            laserProjectiles.Add(Instantiate(laserProjectilePrefab, storingPosition,
                Quaternion.identity, laserProjectilesParent.transform));
            laserProjectiles[i].gameObject.SetActive(false);
        }
        #endregion

        #region Ghosts
        BuggyGhost = Instantiate(buggyGhostPrefab, storingPosition,
                Quaternion.identity, ghostsParent.transform);
        BuggyGhost.gameObject.SetActive(false);
        CopterGhost = Instantiate(copterGhostPrefab, storingPosition,
                Quaternion.identity, ghostsParent.transform);
        CopterGhost.gameObject.SetActive(false);
        MGTurretGhost = Instantiate(mgGhostPrefab, storingPosition,
                Quaternion.identity, ghostsParent.transform);
        MGTurretGhost.gameObject.SetActive(false);
        LaserTurretGhost = Instantiate(laserGhostPrefab, storingPosition,
                Quaternion.identity, ghostsParent.transform);
        LaserTurretGhost.gameObject.SetActive(false);
        PlantGhost = Instantiate(plantGhostPrefab, storingPosition,
                Quaternion.identity, ghostsParent.transform);
        PlantGhost.gameObject.SetActive(false);
        #endregion

        #region Particles
        smallExplosions = new List<ParticleSystem>(numberOfSmallExplosions);
        for (int i = 0; i < numberOfSmallExplosions; i++)
        {
            smallExplosions.Add(Instantiate(smallExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform));
            smallExplosions[i].gameObject.SetActive(false);
        }

        turretExplosions = new List<ParticleSystem>(numberOfTurretExplosions);
        for (int i = 0; i < numberOfTurretExplosions; i++)
        {
            turretExplosions.Add(Instantiate(turretExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform));
            turretExplosions[i].gameObject.SetActive(false);
        }

        bigExplosions = new List<ParticleSystem>(numberOfBigExplosions);
        for (int i = 0; i < numberOfBigExplosions; i++)
        {
            bigExplosions.Add(Instantiate(bigExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform));
            bigExplosions[i].gameObject.SetActive(false);
        }

        enemyShootEffects = new List<ParticleSystem>(numberOfEnemyShootEffects);
        for (int i = 0; i < numberOfEnemyShootEffects; i++)
        {
            enemyShootEffects.Add(Instantiate(enemyShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform));
            enemyShootEffects[i].gameObject.SetActive(false);
        }

        laserShootEffects = new List<ParticleSystem>(numberOfLaserShootEffects);
        for (int i = 0; i < numberOfLaserShootEffects; i++)
        {
            laserShootEffects.Add(Instantiate(laserShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform));
            laserShootEffects[i].gameObject.SetActive(false);
        }

        mgShootEffects = new List<ParticleSystem>(numberOfMGShootEffects);
        for (int i = 0; i < numberOfMGShootEffects; i++)
        {
            mgShootEffects.Add(Instantiate(mgShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform));
            mgShootEffects[i].gameObject.SetActive(false);
        }
        #endregion

        onInitialized?.Invoke();
    }
    public void Reclaim<T>(T poolable) where T : MonoBehaviour, IPoolable
    {
        poolable.ResetValues();
        poolable.transform.position = storingPosition;
        poolable.gameObject.SetActive(false);
    }
    public void Reclaim(ParticleSystem particles)
    {
        particles.Stop();
        particles.gameObject.transform.position = storingPosition;
        particles.gameObject.SetActive(false);
    }
    public void Reclaim<T>(T poolable, float seconds) where T : MonoBehaviour, IPoolable
    {
        StartCoroutine(ReclaimWithDelay(poolable, seconds));
    }
    public void Reclaim(ParticleSystem particles, float seconds)
    {
        StartCoroutine(ReclaimWithDelay(particles, seconds));
    }

    public Buggy GetBuggy()
    {
        for (int i = 0; i < buggies.Count; i++)
        {
            if (!buggies[i].gameObject.activeSelf)
            {
                return buggies[i];
            }
        }
        Buggy buggy = Instantiate(buggyPrefab, storingPosition,
                Quaternion.identity, buggiesParent.transform);
        buggy.gameObject.SetActive(false);
        buggies.Add(buggy);
        return buggy;
    }
    public Copter GetCopter()
    {
        for (int i = 0; i < copters.Count; i++)
        {
            if (!copters[i].gameObject.activeSelf)
            {
                return copters[i];
            }
        }
        Copter copter = Instantiate(copterPrefab, storingPosition,
                Quaternion.identity, coptersParent.transform);
        copter.gameObject.SetActive(false);
        copters.Add(copter);
        return copter;
    }
    public MachineGunTurret GetMG()
    {
        for (int i = 0; i < machineGuns.Count; i++)
        {
            if (!machineGuns[i].gameObject.activeSelf)
            {
                return machineGuns[i];
            }
        }
        MachineGunTurret mg = Instantiate(mgPrefab, storingPosition,
                Quaternion.identity, mgsParent.transform);
        mg.gameObject.SetActive(false);
        machineGuns.Add(mg);
        return mg;
    }
    public LaserTurret GetLaser()
    {
        for (int i = 0; i < lasers.Count; i++)
        {
            if (!lasers[i].gameObject.activeSelf)
            {
                return lasers[i];
            }
        }
        LaserTurret laser = Instantiate(laserPrefab, storingPosition,
                Quaternion.identity, lasersParent.transform);
        laser.gameObject.SetActive(false);
        lasers.Add(laser);
        return laser;
    }
    public Plant GetPlant()
    {
        for (int i = 0; i < plants.Count; i++)
        {
            if (!plants[i].gameObject.activeSelf)
            {
                return plants[i];
            }
        }
        Plant plant = Instantiate(plantPrefab, storingPosition,
                Quaternion.identity, plantsParent.transform);
        plant.gameObject.SetActive(false);
        plants.Add(plant);
        return plant;
    }

    public Projectile GetBuggyProjectile()
    {
        for (int i = 0; i < buggyProjectiles.Count; i++)
        {
            if (!buggyProjectiles[i].gameObject.activeSelf)
            {
                return buggyProjectiles[i];
            }
        }
        Projectile proj = Instantiate(buggyProjectilePrefab, storingPosition,
                Quaternion.identity, buggyProjectilesParent.transform);
        proj.gameObject.SetActive(false);
        buggyProjectiles.Add(proj);
        return proj;
    }
    public Projectile GetCopterProjectile()
    {
        for (int i = 0; i < copterProjectiles.Count; i++)
        {
            if (!copterProjectiles[i].gameObject.activeSelf)
            {
                return copterProjectiles[i];
            }
        }
        Projectile proj = Instantiate(copterProjectilePrefab, storingPosition,
                Quaternion.identity, copterProjectilesParent.transform);
        proj.gameObject.SetActive(false);
        copterProjectiles.Add(proj);
        return proj;
    }
    public Projectile GetMGProjectile()
    {
        for (int i = 0; i < mgProjectiles.Count; i++)
        {
            if (!mgProjectiles[i].gameObject.activeSelf)
            {
                return mgProjectiles[i];
            }
        }
        Projectile proj = Instantiate(mgProjectilePrefab, storingPosition,
                Quaternion.identity, mgProjectilesParent.transform);
        proj.gameObject.SetActive(false);
        mgProjectiles.Add(proj);
        return proj;
    }
    public Projectile GetLaserProjectile()
    {
        for (int i = 0; i < laserProjectiles.Count; i++)
        {
            if (!laserProjectiles[i].gameObject.activeSelf)
            {
                return laserProjectiles[i];
            }
        }
        Projectile proj = Instantiate(laserProjectilePrefab, storingPosition,
                Quaternion.identity, laserProjectilesParent.transform);
        proj.gameObject.SetActive(false);
        laserProjectiles.Add(proj);
        return proj;
    }

    public ParticleSystem GetSmallExplosion()
    {
        for (int i = 0; i < smallExplosions.Count; i++)
        {
            if (!smallExplosions[i].gameObject.activeSelf)
            {
                return smallExplosions[i];
            }
        }
        ParticleSystem ps = Instantiate(smallExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
        ps.gameObject.SetActive(false);
        smallExplosions.Add(ps);
        return ps;
    }
    public ParticleSystem GetTurretExplosion()
    {
        for (int i = 0; i < turretExplosions.Count; i++)
        {
            if (!turretExplosions[i].gameObject.activeSelf)
            {
                return turretExplosions[i];
            }
        }
        ParticleSystem ps = Instantiate(turretExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
        ps.gameObject.SetActive(false);
        turretExplosions.Add(ps);
        return ps;
    }
    public ParticleSystem GetBigExplosion()
    {
        for (int i = 0; i < bigExplosions.Count; i++)
        {
            if (!bigExplosions[i].gameObject.activeSelf)
            {
                return bigExplosions[i];
            }
        }
        ParticleSystem ps = Instantiate(bigExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
        ps.gameObject.SetActive(false);
        bigExplosions.Add(ps);
        return ps;
    }
    public ParticleSystem GetEnemyShootEffect()
    {
        for (int i = 0; i < enemyShootEffects.Count; i++)
        {
            if (!enemyShootEffects[i].gameObject.activeSelf)
            {
                return enemyShootEffects[i];
            }
        }
        ParticleSystem ps = Instantiate(enemyShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
        ps.gameObject.SetActive(false);
        enemyShootEffects.Add(ps);
        return ps;
    }
    public ParticleSystem GetMGShootEffect()
    {
        for (int i = 0; i < mgShootEffects.Count; i++)
        {
            if (!mgShootEffects[i].gameObject.activeSelf)
            {
                return mgShootEffects[i];
            }
        }
        ParticleSystem ps = Instantiate(mgShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
        ps.gameObject.SetActive(false);
        mgShootEffects.Add(ps);
        return ps;
    }
    public ParticleSystem GetLaserShootEffect()
    {
        for (int i = 0; i < laserShootEffects.Count; i++)
        {
            if (!laserShootEffects[i].gameObject.activeSelf)
            {
                return laserShootEffects[i];
            }
        }
        ParticleSystem ps = Instantiate(laserShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
        ps.gameObject.SetActive(false);
        laserShootEffects.Add(ps);
        return ps;
    }

    protected override void Awake()
    {
        base.Awake();
        storingPosition = new Vector3(0f, 100f, 0f);
    }
    private IEnumerator ReclaimWithDelay<T>(T poolable, float seconds) where T : MonoBehaviour, IPoolable
    {
        yield return new WaitForSeconds(seconds);
        Reclaim(poolable);
    }
    private IEnumerator ReclaimWithDelay(ParticleSystem particles, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Reclaim(particles);
    }
}
