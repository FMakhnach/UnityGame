using UnityEngine;
using System;

public abstract class Unit : MonoBehaviour, IDamageable, ITarget
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
    protected ITarget currentTarget;
    private float timer;
    private LayerMask targetableMask;
    private Alignment alignment;

    /// <summary>
    /// Audio source for sound effects.
    /// </summary>
    private AudioSource audioSource;
    private float currentHealth;

    private bool pathIsNotComplete;
    private Action currentBehavior;
    /// <summary>
    /// Path that the unit is following
    /// </summary>
    private Vector3[] path;
    /// <summary>
    /// Current destination point index in array.
    /// </summary>
    private int curDestId;
    private float currentSpeed;
    private Collider previousColliderHit;

    public Alignment Alignment => alignment;
    public Transform TargetPoint => ownTarget;

    /// <summary>
    /// Moves to the spawn point and takes path info from it.
    /// </summary> 
    public void SpawnOn(Spawn spawn, Alignment alignment)
    {
        this.alignment = alignment;
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        audioSource.PlayOneShot(config.spawnSound, 0.3f);

        path = spawn.GetRoad();
        currentBehavior = MovingBehavior;
        curDestId = 0;
        pathIsNotComplete = true;
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
            if (path[curDestId] != transform.position)
            {
                transform.rotation = Quaternion.LookRotation(path[curDestId] - transform.position);
            }
            Vector3 deltaPath = path[curDestId] - transform.position;
            transform.position += deltaPath.normalized * currentSpeed * Time.deltaTime;
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
    /// <summary>
    /// Aims at the target.
    /// </summary>
    protected abstract void Aim();
    /// <summary>
    /// Attacks the target.
    /// </summary>
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
            transform.rotation = Quaternion.LookRotation(path[curDestId], Vector3.up);
            currentBehavior = MovingBehavior;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoadNode>() != null && previousColliderHit != other)
        {
            if (curDestId == path.Length - 1)
            {
                pathIsNotComplete = false;
                Debug.Log("Quest Completed!");
            }
            else
            {
                curDestId++;
                // Just not to double-update index.
                previousColliderHit = other;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}
