using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Number of seconds after which the projectile self-destroys.
    /// </summary>
    [SerializeField]
    private float lifeTime = 1f;
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
    /// The direction in which the projectile will fly.
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// Initializing a projectile by giving it all what it needs.
    /// </summary>
    public void Initialize(Vector3 direction, float damage, PlayerManager owner, ParticleSystem particles)
    {
        this.direction = direction.normalized;
        this.owner = owner;
        this.damage = damage;
        this.particles = particles;
        Destroy(gameObject, lifeTime);
        Destroy(particles.gameObject, lifeTime);
    }

    private void Update()
    {
        // Just flying towards the destination
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        // Check if we hit an enemy
        if (damageable != null && damageable.Owner != owner)
        {
            damageable.ReceiveDamage(damage, owner);
            particles.Stop();
            Destroy(particles.gameObject);
            gameObject.SetActive(false);
        }
    }
}