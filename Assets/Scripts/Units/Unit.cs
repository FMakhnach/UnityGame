using UnityEngine;
using System;

public abstract class Unit : MonoBehaviour, ISpawnable<SpawnTile>, IDamageable, ITarget
{
    /// <summary>
    /// Unit's stats and stuff.
    /// </summary>
    [SerializeField]
    private UnitConfiguration config;
    /// <summary>
    /// Unit's own target point.
    /// </summary>
    [SerializeField]
    private Transform ownTarget;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private ParticleSystem fireParticles;
    /// <summary>
    /// Current destination.
    /// </summary>
    private Tile destination;
    /// <summary>
    /// Path that the unit is following
    /// </summary>
    private Tile[] path;
    /// <summary>
    /// Current destination point index in array.
    /// </summary>
    private int curDestId;
    /// <summary>
    /// Audio source for sound effects.
    /// </summary>
    private AudioSource audioSource;
    private float currentHealth;
    private float currentSpeed;
    private bool pathIsNotComplete;
    private Alignment alignment;

    private Action currentBehavior;
    protected ITarget currentTarget;
    private float timer;
    private LayerMask targetableMask;

    public Alignment Alignment => alignment;
    public Transform TargetPoint => ownTarget;

    public void SpawnOn(SpawnTile spawn, Alignment alignment)
    {
        this.alignment = alignment;
        path = spawn.Path;
        destination = path[0];
        pathIsNotComplete = true;
        transform.position = spawn.transform.position;
        transform.LookAt(destination.transform);
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
        curDestId = 0;
    }
    public void RecieveDamage(float damage)
    {
        if (gameObject != null && damage >= currentHealth)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        currentHealth -= damage;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = config.health;
        currentSpeed = config.speed;

        currentBehavior = MovingBehavior;
        targetableMask = LayerMask.GetMask("Targetables");
        timer = config.attackingInterval;
    }
    private void Update()
    {
        currentBehavior?.Invoke();
    }
    /// <summary>
    /// Moves the unit towards the destination point.
    /// </summary>
    private void MoveToDestination()
    {
        if (pathIsNotComplete)
        {
            Vector3 deltaPath = destination.transform.position - transform.position;
            deltaPath.y = 0;
            transform.position += deltaPath.normalized * currentSpeed * Time.deltaTime;

            if ((transform.position - destination.transform.position).sqrMagnitude < 0.001f)
            {
                if (destination == path[path.Length - 1])
                {
                    pathIsNotComplete = false;
                    Debug.Log("Quest Completed!");
                }
                else
                {
                    destination = path[++curDestId];
                    transform.LookAt(destination.transform);
                }
            }
        }
    }

    private void ScanTerritory()
    {
        float dist, minDistance = float.MaxValue;
        ITarget current;
        var colliders = Physics.OverlapSphere(transform.position, config.radius, targetableMask);
        if (colliders != null)
        {
            foreach (var col in colliders)
            {
                current = col.gameObject.GetComponentInParent<ITarget>();
                if (current != null && current.Alignment != Alignment)
                {
                    dist = Vector3.Distance(current.TargetPoint.position, transform.position);
                    if (dist < minDistance)
                    {
                        currentTarget = current;
                        minDistance = dist;
                    }
                }
            }
            if (currentTarget != null)
            {
                Aim();
                timer = config.attackingInterval;
                currentBehavior = AttackingBehavior;
            }
        }
    }
    protected abstract void Aim();
    private void Fire()
    {
        var rot = fireParticles.transform.rotation.eulerAngles;
        fireParticles.transform.LookAt(currentTarget.TargetPoint);
        fireParticles.transform.rotation = Quaternion.Euler(rot.x, transform.rotation.eulerAngles.y, rot.z);

        fireParticles.Play();
        audioSource.PlayOneShot(config.attackSound, 0.3f);

        Projectile proj = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        var direction = currentTarget.TargetPoint.position - proj.transform.position;
        proj.Initialize(direction, config.damage, Alignment);
    }
    private void MovingBehavior()
    {
        MoveToDestination();
        ScanTerritory();
    }
    private void AttackingBehavior()
    {
        if (currentTarget != null)
        {
            // Super fucking weird thing lol
            // There's something wrong with object destroying
            if (currentTarget.ToString() == "null")
            {
                currentTarget = null;
                return;
            }
            if (timer < config.attackingInterval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer -= config.attackingInterval;
                Fire();
            }
        }
        else
        {
            transform.LookAt(destination.transform);
            currentBehavior = MovingBehavior;
        }
    }
}
