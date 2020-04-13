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
    [SerializeField]
    private float damage;

    /// <summary>
    /// The direction in which the projectile will fly.
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// We don't want to harm ourselves I guess.
    /// </summary>
    private Alignment alignment;

    /// <summary>
    /// Initializing a projectile by giving it a direction and an alignment.
    /// </summary>
    public void Initialize(Vector3 direction, Alignment alignment)
    {
        this.direction = direction.normalized;
        this.alignment = alignment;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null && damageable.Alignment != alignment)
        {
            damageable.RecieveDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
