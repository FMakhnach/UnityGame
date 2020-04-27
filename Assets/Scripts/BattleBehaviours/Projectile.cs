using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Number of seconds after which the projectile self-destroys.
    /// </summary>
    [SerializeField]
    private float lifeTime = 1f;
    /// <summary>
    /// Speed of the projectile.
    /// </summary>
    [SerializeField]
    private float speed;
    /// <summary>
    /// Damage that the projectile causes.
    /// </summary>
    private float damage;
    private PlayerManager owner;

    /// <summary>
    /// The direction in which the projectile will fly.
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// Initializing a projectile by giving it a direction and an alignment.
    /// </summary>
    public void Initialize(Vector3 direction, float damage, PlayerManager owner)
    {
        this.direction = direction.normalized;
        this.owner = owner;
        this.damage = damage;
        Destroy(gameObject, lifeTime);
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
            damageable.ReceiveDamage(damage);
            gameObject.SetActive(false);
        }
    }
}