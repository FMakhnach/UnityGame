using UnityEngine;

public class Tower : MonoBehaviour, ISpawnable<TowerTile>, ITarget
{
    [SerializeField]
    protected TowerConfiguration config;
    [SerializeField]
    private GameObject ownTarget;
    private LayerMask targetableMask;
    private AudioSource audioSource;
    private Alignment alignment;

    public Transform TargetPoint => ownTarget.transform;
    protected LayerMask TargetableMask => targetableMask;
    public Alignment Alignment => alignment;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
    }
    public void SpawnOn(TowerTile tile, Alignment alignment)
    {
        audioSource.PlayOneShot(config.spawnSound, 0.3f);
        transform.position = tile.transform.position;
        this.alignment = alignment;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}
