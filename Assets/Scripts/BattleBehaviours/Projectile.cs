using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    /// <summary>
    /// Number of seconds after which the projectile self-destroys.
    /// </summary>
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float speed;
    private float damage;
    /// <summary>
    /// To check if we hit an enemy.
    /// </summary>
    private PlayerManager owner;
    /// <summary>
    /// Destroying this on collision.
    /// </summary>
    private ParticleSystem particles;
    /// <summary>
    /// direction * speed * Time.deltaTime
    /// </summary>
    private Vector3 directionTimesSpeed;

    /// <summary>
    /// Initializing a projectile by giving it all what it needs.
    /// </summary>
    public void Initialize(Vector3 direction, float damage, PlayerManager owner, ParticleSystem particles)
    {
        directionTimesSpeed = direction.normalized * speed;
        this.owner = owner;
        this.damage = damage;
        this.particles = particles;
        // It should be destroyed in time.
        PoolManager.Instance.Reclaim(this, lifeTime);
        PoolManager.Instance.Reclaim(particles, lifeTime);
        gameObject.SetActive(true);
    }
    public void ResetValues()
    {
        damage = default;
        owner = default;
        particles = default;
        directionTimesSpeed = default;
    }

    private void Update()
    {
        // Just flying in given direction.
        transform.Translate(directionTimesSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        // Check if we hit an enemy
        if (damageable != null && damageable.Owner != owner)
        {
            damageable.ReceiveDamage(damage, owner);
            PoolManager.Instance.Reclaim(particles);
            PoolManager.Instance.Reclaim(this);
        }
    }
}