using TMPro;
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
    private DamageableBehaviour damageableBehaviour;

    public Transform TargetPoint => ownTarget;
    public Alignment Alignment => alignment;

    protected virtual void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();
        audioSource = GetComponent<AudioSource>();
        targetableMask = LayerMask.GetMask("Targetables");
    }
    public void PlaceOn(TowerPlacement place, Alignment alignment)
    {
        this.alignment = alignment;
        transform.position = place.transform.position;
        transform.rotation = place.Rotation;
        audioSource.PlayOneShot(config.spawnSound, 0.3f * audioSource.volume);
    }

    public void ReceiveDamage(float damage)
    {
        if (damageableBehaviour.ReceiveDamage(damage))
        {
            Destroy(this.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, config.radius);
    }
#endif
}
