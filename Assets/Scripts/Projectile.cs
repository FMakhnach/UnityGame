using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1f;
    [SerializeField]
    private float speed;

    private Vector3 direction;
    private float damage;
    private Alignment alignment;

    public void Initialize(Vector3 direction, float damage, Alignment alignment)
    {
        this.direction = direction.normalized;
        this.damage = damage;
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
