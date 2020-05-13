using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private Buggy[] buggies;
    private Copter[] copters;
    private MachineGunTurret[] machineGuns;
    private LaserTurret[] lasers;
    private Plant[] plants;
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

    private Projectile[] buggyProjectiles;
    private Projectile[] copterProjectiles;
    private Projectile[] mgProjectiles;
    private Projectile[] laserProjectiles;
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

    private ParticleSystem[] smallExplosions;
    private ParticleSystem[] turretExplosions;
    private ParticleSystem[] bigExplosions;
    private ParticleSystem[] enemyShootEffects;
    private ParticleSystem[] laserShootEffects;
    private ParticleSystem[] mgShootEffects;
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
        buggies = new Buggy[numberOfBuggies];
        for (int i = 0; i < buggies.Length; i++)
        {
            buggies[i] = Instantiate(buggyPrefab, storingPosition,
                Quaternion.identity, buggiesParent.transform);
            buggies[i].gameObject.SetActive(false);
        }

        copters = new Copter[numberOfCopters];
        for (int i = 0; i < copters.Length; i++)
        {
            copters[i] = Instantiate(copterPrefab, storingPosition,
                Quaternion.identity, coptersParent.transform);
            copters[i].gameObject.SetActive(false);
        }

        machineGuns = new MachineGunTurret[numberOfMachineGuns];
        for (int i = 0; i < machineGuns.Length; i++)
        {
            machineGuns[i] = Instantiate(mgPrefab, storingPosition,
                Quaternion.identity, mgsParent.transform);
            machineGuns[i].gameObject.SetActive(false);
        }

        lasers = new LaserTurret[numberOfLasers];
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i] = Instantiate(laserPrefab, storingPosition,
                Quaternion.identity, lasersParent.transform);
            lasers[i].gameObject.SetActive(false);
        }

        plants = new Plant[numberOfPlants];
        for (int i = 0; i < plants.Length; i++)
        {
            plants[i] = Instantiate(plantPrefab, storingPosition,
                Quaternion.identity, plantsParent.transform);
            plants[i].gameObject.SetActive(false);
        }
        #endregion

        #region Projectile
        buggyProjectiles = new Projectile[numberOfBuggyProjectiles];
        for (int i = 0; i < buggyProjectiles.Length; i++)
        {
            buggyProjectiles[i] = Instantiate(buggyProjectilePrefab, storingPosition,
                Quaternion.identity, buggyProjectilesParent.transform);
            buggyProjectiles[i].gameObject.SetActive(false);
        }

        copterProjectiles = new Projectile[numberOfCopterProjectiles];
        for (int i = 0; i < copterProjectiles.Length; i++)
        {
            copterProjectiles[i] = Instantiate(copterProjectilePrefab, storingPosition,
                Quaternion.identity, copterProjectilesParent.transform);
            copterProjectiles[i].gameObject.SetActive(false);
        }

        mgProjectiles = new Projectile[numberOfMGProjectiles];
        for (int i = 0; i < mgProjectiles.Length; i++)
        {
            mgProjectiles[i] = Instantiate(mgProjectilePrefab, storingPosition,
                Quaternion.identity, mgProjectilesParent.transform);
            mgProjectiles[i].gameObject.SetActive(false);
        }

        laserProjectiles = new Projectile[numberOfLaserProjectiles];
        for (int i = 0; i < laserProjectiles.Length; i++)
        {
            laserProjectiles[i] = Instantiate(laserProjectilePrefab, storingPosition,
                Quaternion.identity, laserProjectilesParent.transform);
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
        smallExplosions = new ParticleSystem[numberOfSmallExplosions];
        for (int i = 0; i < smallExplosions.Length; i++)
        {
            smallExplosions[i] = Instantiate(smallExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
            smallExplosions[i].gameObject.SetActive(false);
        }

        turretExplosions = new ParticleSystem[numberOfTurretExplosions];
        for (int i = 0; i < turretExplosions.Length; i++)
        {
            turretExplosions[i] = Instantiate(turretExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
            turretExplosions[i].gameObject.SetActive(false);
        }

        bigExplosions = new ParticleSystem[numberOfBigExplosions];
        for (int i = 0; i < bigExplosions.Length; i++)
        {
            bigExplosions[i] = Instantiate(bigExplosionPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
            bigExplosions[i].gameObject.SetActive(false);
        }

        enemyShootEffects = new ParticleSystem[numberOfEnemyShootEffects];
        for (int i = 0; i < enemyShootEffects.Length; i++)
        {
            enemyShootEffects[i] = Instantiate(enemyShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
            enemyShootEffects[i].gameObject.SetActive(false);
        }

        laserShootEffects = new ParticleSystem[numberOfLaserShootEffects];
        for (int i = 0; i < laserShootEffects.Length; i++)
        {
            laserShootEffects[i] = Instantiate(laserShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
            laserShootEffects[i].gameObject.SetActive(false);
        }

        mgShootEffects = new ParticleSystem[numberOfMGShootEffects];
        for (int i = 0; i < mgShootEffects.Length; i++)
        {
            mgShootEffects[i] = Instantiate(mgShootPrefab, storingPosition,
                Quaternion.identity, particlesParent.transform);
            mgShootEffects[i].gameObject.SetActive(false);
        }
        #endregion

        onInitialized?.Invoke();
    }
    public void Reclaim(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = storingPosition;
    }
    public void Reclaim(GameObject gameObject, float seconds)
    {
        StartCoroutine(ReclaimWithDelay(gameObject, seconds));
    }

    public Buggy GetBuggy()
    {
        for (int i = 0; i < buggies.Length; i++)
        {
            if (!buggies[i].gameObject.activeSelf)
            {
                return buggies[i];
            }
        }
        return null;
    }
    public Copter GetCopter()
    {
        for (int i = 0; i < copters.Length; i++)
        {
            if (!copters[i].gameObject.activeSelf)
            {
                return copters[i];
            }
        }
        return null;
    }
    public MachineGunTurret GetMG()
    {
        for (int i = 0; i < machineGuns.Length; i++)
        {
            if (!machineGuns[i].gameObject.activeSelf)
            {
                return machineGuns[i];
            }
        }
        return null;
    }
    public LaserTurret GetLaser()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if (!lasers[i].gameObject.activeSelf)
            {
                return lasers[i];
            }
        }
        return null;
    }
    public Plant GetPlant()
    {
        for (int i = 0; i < plants.Length; i++)
        {
            if (!plants[i].gameObject.activeSelf)
            {
                return plants[i];
            }
        }
        return null;
    }

    public Projectile GetBuggyProjectile()
    {
        for (int i = 0; i < buggyProjectiles.Length; i++)
        {
            if (!buggyProjectiles[i].gameObject.activeSelf)
            {
                return buggyProjectiles[i];
            }
        }
        return null;
    }
    public Projectile GetCopterProjectile()
    {
        for (int i = 0; i < copterProjectiles.Length; i++)
        {
            if (!copterProjectiles[i].gameObject.activeSelf)
            {
                return copterProjectiles[i];
            }
        }
        return null;
    }
    public Projectile GetMGProjectile()
    {
        for (int i = 0; i < mgProjectiles.Length; i++)
        {
            if (!mgProjectiles[i].gameObject.activeSelf)
            {
                return mgProjectiles[i];
            }
        }
        return null;
    }
    public Projectile GetLaserProjectile()
    {
        for (int i = 0; i < laserProjectiles.Length; i++)
        {
            if (!laserProjectiles[i].gameObject.activeSelf)
            {
                return laserProjectiles[i];
            }
        }
        return null;
    }

    public ParticleSystem GetSmallExplosion()
    {
        for (int i = 0; i < smallExplosions.Length; i++)
        {
            if (!smallExplosions[i].gameObject.activeSelf)
            {
                return smallExplosions[i];
            }
        }
        return null;
    }
    public ParticleSystem GetTurretExplosion()
    {
        for (int i = 0; i < turretExplosions.Length; i++)
        {
            if (!turretExplosions[i].gameObject.activeSelf)
            {
                return turretExplosions[i];
            }
        }
        return null;
    }
    public ParticleSystem GetBigExplosion()
    {
        for (int i = 0; i < bigExplosions.Length; i++)
        {
            if (!bigExplosions[i].gameObject.activeSelf)
            {
                return bigExplosions[i];
            }
        }
        return null;
    }
    public ParticleSystem GetEnemyShootEffect()
    {
        for (int i = 0; i < enemyShootEffects.Length; i++)
        {
            if (!enemyShootEffects[i].gameObject.activeSelf)
            {
                return enemyShootEffects[i];
            }
        }
        return null;
    }
    public ParticleSystem GetMGShootEffect()
    {
        for (int i = 0; i < mgShootEffects.Length; i++)
        {
            if (!mgShootEffects[i].gameObject.activeSelf)
            {
                return mgShootEffects[i];
            }
        }
        return null;
    }
    public ParticleSystem GetLaserShootEffect()
    {
        for (int i = 0; i < laserShootEffects.Length; i++)
        {
            if (!laserShootEffects[i].gameObject.activeSelf)
            {
                return laserShootEffects[i];
            }
        }
        return null;
    }

    protected override void Awake()
    {
        base.Awake();
        storingPosition = new Vector3(0f, 100f, 0f);
    }
    private IEnumerator ReclaimWithDelay(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Reclaim(gameObject);
    }
}
