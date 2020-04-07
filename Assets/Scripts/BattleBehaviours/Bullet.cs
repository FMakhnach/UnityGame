using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float lifeTime = 1.5f;

    private Vector3 direction;
    private float damage;
    private float speed;
    private Alignment alignment;
    private float timer;

    public void Initialize(Vector3 direction, float damage, float speed, Alignment alignment)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.speed = speed;
        this.alignment = alignment;
        timer = 0f;
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
            Destroy(gameObject);
        }
    }
}
