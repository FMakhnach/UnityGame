using System;
using UnityEngine;
using UnityEngine.AI;

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
    protected GameObject body;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private ParticleSystem fireParticles;
    protected ITarget currentTarget;
    private float timer;
    private LayerMask targetableMask;
    private PlayerManager owner;
    protected DamageableBehaviour damageableBehaviour;
    /// <summary>
    /// Audio source for sound effects.
    /// </summary>
    private AudioSource audioSource;

    private Action currentBehavior;
    /// <summary>
    /// Path that the unit is following
    /// </summary>
    private Vector3[] path;
    /// <summary>
    /// Current destination point index in array.
    /// </summary>
    private int curDestId;
    private NavMeshAgent agent;

    public PlayerManager Owner
    {
        get => owner;
        set
        {
            if (owner == null && value != null)
            {
                owner = value;
            }
        }
    }
    public Transform TargetPoint => ownTarget;
    public UnitInfoPanel Panel { get; protected set; }

    /// <summary>
    /// Moves to the spawn point and takes path info from it.
    /// </summary> 
    public void SpawnOn(Spawn spawn)
    {
        transform.position = spawn.transform.position;
        transform.rotation = spawn.transform.rotation;
        audioSource.PlayOneShot(config.spawnSound, 0.3f * audioSource.volume);

        path = spawn.GetRoad();
        currentBehavior = MovingBehavior;
        curDestId = 0;
        agent.enabled = true;
        agent.SetDestination(path[curDestId]);
    }
    public void ReceiveDamage(float damage, PlayerManager from)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            from.UnitKilled();
            owner.UnitLost();
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = config.speed;
        targetableMask = LayerMask.GetMask("Targetables");
        timer = config.attackingInterval;
    }
    private void Update()
    {
        currentBehavior?.Invoke();
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
                if (current != null && current.Owner != Owner)
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
                timer = config.attackingInterval;
                currentBehavior = AttackingBehavior;
                agent.enabled = false;
            }
        }
    }
    /// <summary>
    /// Aims at the target.
    /// </summary>
    protected abstract void Aim();
    /// <summary>
    /// Fires the projectile at the target with particles and sfx.
    /// </summary>
    private void Fire()
    {
        var fireParticles = Instantiate(this.fireParticles,
                                        this.fireParticles.transform.position,
                                        this.fireParticles.transform.rotation);
        fireParticles.transform.LookAt(currentTarget.TargetPoint);

        fireParticles.Play();
        Destroy(fireParticles.gameObject, 1f);
        audioSource.PlayOneShot(config.attackSound, 0.3f * audioSource.volume);

        Projectile proj = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        var direction = currentTarget.TargetPoint.position - proj.transform.position;
        proj.Initialize(direction, config.damage, owner, fireParticles);
    }
    private void MovingBehavior()
    {
        if ((transform.position - path[curDestId]).sqrMagnitude < 0.5f)
        {
            if (curDestId == path.Length - 1)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.SetDestination(path[++curDestId]);
            }
        }
        ScanTerritory();
    }
    private void AttackingBehavior()
    {
        if (currentTarget != null)
        {
            // Super fucking weird thing lol
            // There's something wrong with object destroying
            if (currentTarget.ToString() == "null"/*
                || (currentTarget.TargetPoint.position - transform.position).magnitude > (config.radius + 0.25f)*/)
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
                Aim();
                Fire();
            }
        }
        else
        {
            currentBehavior = MovingBehavior;
            body.transform.localRotation = Quaternion.identity;
            agent.enabled = true;
            agent.SetDestination(path[curDestId]);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}
