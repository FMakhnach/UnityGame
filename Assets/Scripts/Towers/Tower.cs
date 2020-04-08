using UnityEngine;

public abstract class Tower : MonoBehaviour, ISpawnable<TowerTile>, ITarget
{
    [SerializeField]
    protected TowerConfiguration config;
    [SerializeField]
    private GameObject ownTarget;
    protected LayerMask targetableMask;
    protected AudioSource audioSource;
    private Alignment alignment;

    public Transform TargetPoint => ownTarget.transform;
    public Alignment Alignment => alignment;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
    }
    public void SpawnOn(TowerTile tile, Alignment alignment)
    {
        transform.position = tile.transform.position;
        this.alignment = alignment;
        tile.RecieveTower(this);
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}
