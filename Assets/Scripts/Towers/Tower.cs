using UnityEngine;

public abstract class Tower : MonoBehaviour, ITarget, IDamageable
{
    [SerializeField]
    protected TowerConfiguration config;
    [SerializeField]
    private Transform ownTarget;
    protected LayerMask targetableMask;
    protected AudioSource audioSource;
    private Alignment alignment;

    private float currentHealth;

    public Transform TargetPoint => ownTarget;
    public Alignment Alignment => alignment;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
        currentHealth = config.health;
    }
    public void PlaceOn(Vector3 placePoint, Quaternion rotation, Alignment alignment)
    {
        this.alignment = alignment;
        transform.position = placePoint;
        transform.rotation = rotation;
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
    }
    public void RecieveDamage(float damage)
    {
        if (gameObject != null && damage >= currentHealth)
        {
            var ps = Instantiate(config.destroyParticles, transform.position, transform.rotation);
            ps.Play();
            ps.GetComponent<AudioSource>().PlayOneShot(config.destroySound, 0.5f);
            Destroy(ps.gameObject, config.destroySound.length + 0.2f);
            Destroy(this.gameObject);
        }
        currentHealth -= damage;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}
