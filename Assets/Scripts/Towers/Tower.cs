using UnityEngine;

public abstract class Tower : MonoBehaviour, ISpawnable<TowerTile>, ITarget, IDamageable
{
    [SerializeField]
    protected TowerConfiguration config;
    [SerializeField]
    private Transform ownTarget;
    protected LayerMask targetableMask;
    protected AudioSource audioSource;
    private Alignment alignment;

    private float currentHealth;
    private TowerTile tile;

    public Transform TargetPoint => ownTarget;
    public Alignment Alignment => alignment;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
        currentHealth = config.health;
    }
    public void SpawnOn(TowerTile tile, Alignment alignment)
    {
        transform.position = tile.transform.position;
        this.alignment = alignment;
        tile.IsOccupied = true;
        this.tile = tile;
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
    }
    public void RecieveDamage(float damage)
    {
        if (gameObject != null && damage >= currentHealth)
        {
            tile.IsOccupied = false;
            gameObject.SetActive(false);
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
