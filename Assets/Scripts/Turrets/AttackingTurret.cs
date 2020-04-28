using UnityEngine;

public abstract class AttackingTurret : Turret
{
    private const float maxTurretSlope = 10f;

    /// <summary>
    /// Prefab of a projectile the turret shoots.
    /// </summary>
    [SerializeField]
    private Projectile projectilePrefab;
    /// <summary>
    /// Particle effect for firing.
    /// </summary>
    [SerializeField]
    private ParticleSystem fireParticles;
    /// <summary>
    /// Turret transform for rotating.
    /// </summary>
    [SerializeField]
    private Transform turret;
    /// <summary>
    /// The point that the turret shoots from.
    /// </summary>
    [SerializeField]
    private Transform firePoint;
    /// <summary>
    /// The object the turret targets at the moment, if any.
    /// </summary>
    private ITarget currentTarget;
    /// <summary>
    /// Timer for managing attacking intervals.
    /// </summary>
    private float attackTimer;

    /// <summary>
    /// How long we wait after reaching certain rotation.
    /// </summary>
    private float idleCooldown;
    /// <summary>
    /// The next euler Y angle we need to reach.
    /// </summary>
    private float rotY;
    /// <summary>
    /// Direction of rotation on idling. Can be -1 or 1.
    /// </summary>
    private int idleRotationDirection;
    /// <summary>
    /// The speed of rotation on idling.
    /// </summary>
    private float idleRotationSpeed;

    protected override void Awake()
    {
        base.Awake();
        idleRotationDirection = 1;
        idleRotationSpeed = 15f;
        rotY = turret.transform.rotation.eulerAngles.y;
        idleCooldown = 0f;
    }
    private void Update()
    {
        if (currentTarget != null)
        {
            // Super fucking weird thing lol
            // There's something wrong with object destroying
            // Also checking if unit went out of range
            if (currentTarget.ToString() == "null"
                || Vector3.Distance(currentTarget.TargetPoint.position, transform.position) > config.radius)
            {
                currentTarget = null;
                return;
            }

            Aim();
            if (attackTimer < config.attackingInterval)
            {
                attackTimer += Time.deltaTime;
            }
            else
            {
                attackTimer -= config.attackingInterval;
                Fire();
            }
        }
        else
        {
            ScanTerritory();
            Idle();
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
                if (current != null && current.Owner != Owner
                    // TODELETE someday
                    && col.gameObject.GetComponentInParent<Base>() == null
                    )
                {
                    dist = Vector3.Distance(current.TargetPoint.position, transform.position);
                    if (dist < minDistance)
                    {
                        currentTarget = current;
                        minDistance = dist;
                        attackTimer = 0f;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Kinda randomly rotates around the Y axis.
    /// </summary>
    private void Idle()
    {
        // If we are waiting for the next idle movement.
        if (idleCooldown > 0f)
        {
            idleCooldown -= Time.deltaTime;
            return;
        }
        // If we reached rotation we were aimed at.
        if (Mathf.Abs(turret.transform.rotation.eulerAngles.y - rotY) < 0.5f)
        {
            // Waiting period to the next idle.
            idleCooldown = Random.Range(0.7f, 1.2f);
            // 50% chance to invert rotation.
            if (Random.Range(0, 2) == 0)
            {
                idleRotationDirection = -idleRotationDirection;
            }
            rotY = turret.transform.rotation.eulerAngles.y + idleRotationDirection * Random.Range(90, 180);
            if (rotY < 0f)
            {
                rotY += 360f;
            }
            else if (rotY > 360f)
            {
                rotY -= 360f;
            }
        }
        Vector3 deltaRot = new Vector3(0f, idleRotationDirection * idleRotationSpeed * Time.deltaTime, 0f);
        turret.transform.Rotate(deltaRot);
    }
    /// <summary>
    /// Aims at the target.
    /// </summary>
    private void Aim()
    {
        turret.LookAt(currentTarget.TargetPoint.position);
        var curRot = turret.rotation.eulerAngles;
        if (curRot.x > 180f) curRot.x -= 360f;
        turret.rotation = Quaternion.Euler(Mathf.Clamp(curRot.x, -maxTurretSlope, maxTurretSlope), curRot.y, 0f);
    }
    /// <summary>
    /// Fires the projectile at the target with particles and sfx.
    /// </summary>
    private void Fire()
    {
        var fireParticles = Instantiate(this.fireParticles,
                                        this.fireParticles.transform.position,
                                        this.fireParticles.transform.rotation);
        fireParticles.Play();
        audioSource.PlayOneShot(config.attackSound, 0.3f * audioSource.volume);

        Projectile proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, firePoint);
        var direction = currentTarget.TargetPoint.position - proj.transform.position;
        proj.Initialize(direction, config.damage, Owner, fireParticles);
    }
}
